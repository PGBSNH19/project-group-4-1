using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class MarketplaceRepository : Repository, IMarketplaceRepository
    {
        public MarketplaceRepository(NearbyProduceContext context) : base(context)
        {
        }

        public async Task<ICollection<Marketplace>> GetMarketplaces()
        {
            IQueryable<Marketplace> query = _context.Marketplaces;
            return await query.ToArrayAsync();
        }

        public async Task<Marketplace> GetMarketplaceById(int id)
        {
            IQueryable<Marketplace> query = _context.Marketplaces.Where(x => x.MarketplaceID == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
