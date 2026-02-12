using ChalkboardChat.BLL.DTOs.MessageDtos;
using ChalkboardChat.BLL.Interfaces;
using ChalkboardChat.DAL.DATAs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly MessageRepository _messageRepo;
        private readonly AuthDbContext _authContext;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MessageService(MessageRepository messageRepository, AuthDbContext authContext, SignInManager<IdentityUser> signInManager)
        {
            _messageRepo = messageRepository;
            _authContext = authContext;
            _signInManager = signInManager;

        }

        public Task<List<MessageListDto>> GetAllMessages()
        {
            //return await _messageRepo.Messages.OrderByDescending(m => m.Date)
            //    .Select(m => new MessageListDto
            //    {
            //        Id = m.Id,
            //        Date = m.Date,
            //        Message = m.Message,
            //        Username = m.Username
            //    })
            //    .ToListAsync();

            var messages = _messageRepo.GetAllMessagesAsync();
            messages = new MessageListDto
            {
                Id = messages.Id,
                Date = messages.Date,
                Message = messages.Message,
                Username = messages.Username
            };
            if (messages == null)
            {
                throw new Exception("No messages found.");
            }
            return messages;

        }
        public Task<MessageDetailDto> CreateMessage(string message)
        {
            var username = 

            var newMessage = _messageRepo.CreateMessageAsync(username);
            


            //var newMessage = new DAL.Models.MessageModel
            //{
            //    Date = dto.Date,
            //    Message = dto.Message,
            //    Username = dto.Username
            //};
            //await _context.Messages.AddAsync(newMessage);
            //await _context.SaveChangesAsync();

            //return new MessageDetailDto
            //{
            //    Date = newMessage.Date,
            //    Message = newMessage.Message,
            //    Username = newMessage.Username
            //};
        }
    }
}
