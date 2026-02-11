using ChalkboardChat.BLL.DTOs.MessageDtos;
using ChalkboardChat.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public class MessageService : IMessageService
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
                    Id = m.Id,
                    Date = m.Date,
                    Message = m.Message,
                    Username = m.Username
                })
                .ToListAsync();

        }
        public async Task<MessageDetailDto> CreateMessageAsync(CreateMessageDto dto)
        {
            var newMessage = new Message
            {
                Date = dto.Date,
                Message = dto.Message,
                Username = dto.Username
            };
            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();

            return new MessageDetailDto
            {
                Date = newMessage.Date,
                Message = newMessage.Message,
                Username = newMessage.Username
            };
        }
    }
}
