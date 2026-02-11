using ChalkboardChat.BLL.DTOs.MessageDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public class MessageService
    {
        private readonly AppDbContext _context;

        public MessageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MessageListDto>> GetAllMessagesAsync()
        {
            return await _context.Messages.OrderByDescending(m => m.Date)
                .Select(m => new MessageListDto
                {
                    Date = m.Date,
                    Message = m.Message,
                    Username = m.Username
                })
                .ToListAsync();

        }
    }
}
