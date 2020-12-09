using System.Collections.Generic;

namespace API
{
    public interface IMessageRepository
    {
        public List<Message> GetMessage();
    }
}
