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
    public static class BrandContextSeed
    {
        public static async Task SeedDataAsync (IMongoCollection<ProductBrand> productBrands)
        {
            var hasTypes = await productBrands.Find(p => true).AnyAsync();
           if (!hasTypes)
                return;

           var filepath = Path.Combine("Data", "SeedData", "brands.json");

            if(!File.Exists(filepath))
            return;
            
            var brandsData = await File.ReadAllTextAsync(filepath);
            var type = JsonSerializer.Deserialize<IEnumerable<ProductBrand>>(brandsData);

            if(type?.Any() != true)
            {
                await productBrands.InsertManyAsync(type);
            }


        }
    }
}
