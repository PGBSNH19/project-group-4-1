using System.Threading.Tasks;
using API.Context;

namespace API.Services
{
    public class Repository : IRepository
    {
        protected readonly NearbyProduceContext _context;

        public Repository(NearbyProduceContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }
    }
}
