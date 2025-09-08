using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IProductBrand, IProductType
    {
        public ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deletedProduct =
                await _context.Products
                .DeleteOneAsync(p=>p.Id==id);

            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount>0;
        }

        public  async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductBrand>> GetProductBrands()
        {
            return await _context.Brands
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrandName(string name)
        {
            return await _context.Products
                .Find(p => p.productBrand.Name==name)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByTypeName(string name)
        {
            return await _context.Products
                           .Find(p => p.productType.Name==name)
                           .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetProductTypes()
        {
            return await _context.Types
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateProduct = await _context.Products
                .ReplaceOneAsync(filter: g => g.Id == product.Id, product);

             return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0;
        }
    }
}
