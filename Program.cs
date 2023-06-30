using AutoMapper;
using DaoDbNorthwind.config;
using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.DaoImplementation;
using DaoDbNorthwind.DaoImplementation;

namespace DaoDbNorthwind
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //DI
            builder.Services.AddScoped<IDaoEmployees, EmplImpl>();
            builder.Services.AddScoped<IDaoOrders, OrdersImpl>();
            builder.Services.AddScoped<IDaoProducts, ProductsImpl>();
            builder.Services.AddScoped<IDaoSupliers, SupliersImpl>();
            builder.Services.AddScoped<IMapperConfig, MapperConfig>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}