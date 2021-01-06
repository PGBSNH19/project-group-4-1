﻿using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class MarketplaceRepository : Repository, IMarketplaceRepository
    {
        public MarketplaceRepository(NearbyProduceContext context) : base(context)
        {
        }

        public async Task<ICollection<Marketplace>> GetMarketplaces()
        {
            IQueryable<Marketplace> query = _context.Marketplaces
                .Include(MarketplaceSeller => MarketplaceSeller.MarketplaceSellers)
                .ThenInclude(MarketplaceSellers => MarketplaceSellers.Seller);
            return await query.ToArrayAsync();
        }

        public async Task<Marketplace> GetMarketplaceById(int id)
        {
            IQueryable<Marketplace> query = _context.Marketplaces.Where(x => x.MarketplaceID == id)
                .Include(MarketplaceSellers => MarketplaceSellers.MarketplaceSellers)
                .ThenInclude(MarketplaceSellers => MarketplaceSellers.Seller);
            return await query.FirstOrDefaultAsync();
        }
    }
}
