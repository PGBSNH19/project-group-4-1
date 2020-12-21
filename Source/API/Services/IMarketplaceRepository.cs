using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    public interface IMarketplaceRepository : IRepository
    {
        Task<ICollection<Marketplace>> GetMarketplaces();
        Task<Marketplace> GetMarketplaceById(int id);
    }
}
