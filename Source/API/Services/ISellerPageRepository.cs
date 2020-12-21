using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ISellerPageRepository : IRepository
    {
        Task<ICollection<SellerPage>> GetSellerPages();

        Task<SellerPage> GetSellerPageByUserID(int id);
    }
}
