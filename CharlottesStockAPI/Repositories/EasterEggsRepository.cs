using ChocolateLib;
using System.Security.Cryptography.X509Certificates;

namespace CharlottesStockAPI.Repositories
{
    public class EasterEggsRepository
    {
        
        public List<EasterEgg> _data;
        public EasterEggsRepository()

        {
            _data = new List<EasterEgg>()
            {


            new EasterEgg { ProductNo = 1, ChocolateType = "Milk", Price = 10, InStock = 10 },
            new EasterEgg { ProductNo = 2, ChocolateType = "Dark", Price = 10, InStock = 10 },
            new EasterEgg { ProductNo = 3, ChocolateType = "White", Price = 10, InStock = 10 }

        };
        }
        public List<EasterEgg> GetAll()
        {
            return new List<EasterEgg>(_data);
        }
        public EasterEgg? GetByProductNo(int productNo)
        {
            var egg = _data.Find(x => x.ProductNo == productNo);
            if (egg == null)
            {
                throw new ArgumentException("Product Number doenst exist");
            }
            else
            {
                return egg;
            }
        }

        public List<EasterEgg> GetLowStock(int stockLevel)
        {
            return _data.Where(egg => egg.InStock <= stockLevel).ToList();
        }
        public void Update(EasterEgg egg)
        {
            var existingEgg = _data.Find(x => x.ProductNo == egg.ProductNo);
            if (existingEgg == null)
            {
                throw new ArgumentException("Product Number does not exist");
            }
            existingEgg.InStock = egg.InStock;
            existingEgg.Price = egg.Price;
            existingEgg.ChocolateType = egg.ChocolateType;
        }




    }
}

    

