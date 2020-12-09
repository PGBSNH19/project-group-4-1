using System.Collections.Generic;

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
