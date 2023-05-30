using CharlottesStockAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<EasterEggsRepository>(new EasterEggsRepository());

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
