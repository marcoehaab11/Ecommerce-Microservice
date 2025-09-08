using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class CatalogContextSeed
    {
        public static async Task SeedDataAsync (IMongoCollection<Product> productcollection)
        {
            var hasproducts = await productcollection.Find(p => true).AnyAsync();
           if (!hasproducts)
                return;

           var filepath = Path.Combine("Data", "SeedData", "products.json");

            if(!File.Exists(filepath))
                return;
            
            var productData = await File.ReadAllTextAsync(filepath);
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productData);

            if(products?.Any() != true)
            {
                await productcollection.InsertManyAsync(products);
            }


        }
    }
}
