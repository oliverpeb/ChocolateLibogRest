using CharlottesStockAPI.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ChocolateLib;

namespace CharlottesStockAPI.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    
    public class EggsController : ControllerBase
    {
        private readonly EasterEggsRepository _repository;
        public EggsController()
        {
            _repository = new EasterEggsRepository();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<EasterEgg>> GetAll()
        {
            List<EasterEgg> result = _repository.GetAll();
            if (result.Count < 1)
            {
                return NoContent();
            }
            return Ok(result);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("productNo")]
        public ActionResult<EasterEgg> Get(int productNo)
        {
            try
            {
                return Ok(_repository.GetByProductNo(productNo));

            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("stocklevel")]
        public ActionResult<IEnumerable<EasterEgg>> GetStockByStockLevel(int? stockLevel)
        {
            List<EasterEgg> result;
            if (stockLevel.HasValue)
            {
                result = _repository.GetLowStock(stockLevel.Value);
            } else
            {
                result = _repository.GetAll();
            }
            if(result.Count < 1)
            {
                return NoContent();
            }
            return Ok(result);

        
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public ActionResult<EasterEgg> Put(int ProductNo, EasterEgg updatedEgg)
        {
            try
            {
                var existingEgg = _repository.GetByProductNo(ProductNo);
                if (existingEgg == null)
                {
                    return NotFound();
                }
                existingEgg.ChocolateType = updatedEgg.ChocolateType;
                existingEgg.Price = updatedEgg.Price;
                existingEgg.InStock = updatedEgg.InStock;

                return Ok(updatedEgg);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);

            }


        }

    }
}
