using CatalogAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CatalogAPI.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            //check jika datanya ada
            bool existProduct = productCollection.Find(p => true).Any();

            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfigureProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfigureProducts()
        {
            return new List<Product>() {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f1",
                    Name = "iPhone X",
                    Summary = "This is description of summary",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                    ImageFile = "product-1.png",
                    Price = 799.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f2",
                    Name = "Samsung 10",
                    Summary = "This is description of summary",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                    ImageFile = "product-1.png",
                    Price = 899.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f3",
                    Name = "Huawei Plus",
                    Summary = "This is description of summary",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                    ImageFile = "product-3.png",
                    Price = 550.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f4",
                    Name = "Xiaomi Mi 9",
                    Summary = "This is description of summary",
                    Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                    ImageFile = "product-3.png",
                    Price = 399.00M,
                    Category = "White Appliances"
                }
            };
        }
    }
}
