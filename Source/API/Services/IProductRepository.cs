using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;


namespace API.Services
{
    public interface IProductRepository : IRepository
    {
        Task<ICollection<Product>> GetProducts();
        Task<Product> GetProductById(int id);
    }
}
