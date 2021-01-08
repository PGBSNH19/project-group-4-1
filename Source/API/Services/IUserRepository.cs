using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IUserRepository : IRepository
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        Task<User> GetUserByEmail(string email);
        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

    }
}
