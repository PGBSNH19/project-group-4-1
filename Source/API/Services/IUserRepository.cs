using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IUserRepository : IRepository
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUserById(int id);

    }
}
