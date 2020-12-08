using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class MessageRepository : IMessageRepository
    {
        public List<Message> GetMessage()
        {
            Message message = new Message()
            {
                Id = 1,
                Text = "Hello Farmer World",
            };

            List<Message> messageList = new List<Message>();

            messageList.Add(message);

            return messageList;
        }
    }
}
