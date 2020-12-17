using API.Models;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IMessageRepository
    {
        Task<Message[]> GetMessages();
    }
}
