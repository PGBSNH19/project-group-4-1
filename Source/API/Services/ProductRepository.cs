using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(NearbyProduceContext context) : base(context)
        {
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            IQueryable<Product> query = _context.Products;
            return await query.ToArrayAsync();
        }
        public async Task<ICollection<Product>> GetProductsBySellerPageId(int id)
        {
            IQueryable<Product> query = _context.Products
                                                .Where(x => x.ProductID == id)
                                                .Include(sellerPage => sellerPage.SellerPageProducts);
            return await query.ToArrayAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            IQueryable<Product> query = _context.Products.Where(x => x.ProductID == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
