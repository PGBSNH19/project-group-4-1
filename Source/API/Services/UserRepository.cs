using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(NearbyProduceContext context) : base(context)
        { }

        public async Task<ICollection<User>> GetUsers()
        {
            IQueryable<User> query = _context.Users;
            return await query.ToArrayAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            IQueryable<User> query = _context.Users.Where(x => x.UserID == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByName(string name)
        {
            IQueryable<User> query = _context.Users.Where(x => x.Username == name);
            return await query.FirstOrDefaultAsync();

        }

        public async Task<User> GetUserByEmail(string email)
        {
            IQueryable<User> query = _context.Users.Where(x => x.Email == email);
            return await query.FirstOrDefaultAsync();
        }
    }
}
