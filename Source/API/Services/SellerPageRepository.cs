using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class SellerPageRepository : Repository, ISellerPageRepository
    {
        public SellerPageRepository(NearbyProduceContext context) : base(context)
        {

        }
        public async Task<SellerPage> GetSellerPageByUserID(int id)
        {
            IQueryable<SellerPage> query = _context.SellerPages.Where(x => x.SellerUserID == id)
                                                                .Include(SellerPageProduct => SellerPageProduct.SellerPageProducts)
                                                                .ThenInclude(SellerPageProduct => SellerPageProduct.product);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ICollection<SellerPage>> GetSellerPages()
        {
            IQueryable<SellerPage> query = _context.SellerPages;
            return await query.ToArrayAsync();
        }
    }
}
