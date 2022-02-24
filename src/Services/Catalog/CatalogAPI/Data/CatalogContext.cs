using CatalogAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CatalogAPI.Data
{
    public class CatalogContext : ICatalogContext
    {        
        //create constructor
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            //seeding data into products
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
