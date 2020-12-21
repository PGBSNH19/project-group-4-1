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
            IQueryable<User> query = _context.User;
            return await query.ToArrayAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            IQueryable<User> query = _context.User.Where(x => x.UserID == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
