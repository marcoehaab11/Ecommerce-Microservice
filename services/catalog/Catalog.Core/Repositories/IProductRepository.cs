using Catalog.Core.Entities;
using Catalog.Core.Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetAllProduct(CatalogSpecParams catalogSpecParams);
        Task<Product> GetProductById(string id);
        Task<IEnumerable<Product>> GetProductByTypeName(string name);
        Task<IEnumerable<Product>> GetProductByBrandName(string name);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
