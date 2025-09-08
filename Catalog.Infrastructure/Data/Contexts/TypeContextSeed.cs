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
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync (IMongoCollection<ProductType> producttype)
        {
            var hasTypes = await producttype.Find(p => true).AnyAsync();
           if (!hasTypes)
                return;

           var filepath = Path.Combine("Data", "SeedData", "types.json");

            if(!File.Exists(filepath))
                return;
            
            var typesData = await File.ReadAllTextAsync(filepath);
            var type = JsonSerializer.Deserialize<IEnumerable<ProductType>>(typesData);

            if(type?.Any() != true)
            {
                await producttype.InsertManyAsync(type);
            }


        }
    }
}
