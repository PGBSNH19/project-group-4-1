using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
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
            IQueryable<Marketplace> query = _context.Marketplaces.AsNoTracking();
            return await query.ToArrayAsync();
        }

        public async Task<Marketplace> GetMarketplaceById(int id)
        {
            var query = await _context.Marketplaces.FirstOrDefaultAsync(x => x.MarketplaceID == id);
            return query;
        }
    }
}
