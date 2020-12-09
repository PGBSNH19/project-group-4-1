using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class MessageRepository : IMessageRepository
    {
        protected readonly NearbyProduceContext _context;
        public MessageRepository(NearbyProduceContext context)
        {
            _context = context;
        }
        public async Task<Message[]> GetMessages()
        {
            IQueryable<Message> query = _context.Messages;

            return await query.ToArrayAsync();
        }
    }
}
