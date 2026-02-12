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

        public async Task<List<MessageListDto>> GetAllMessages()
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

            var messages = await _messageRepo.GetAllMessagesAsync();
            

            if (messages == null)
            {
                throw new Exception("No messages found.");
            }
            
            var messagesDto = messages.Select(m => new MessageListDto
            {
                Id = m.Id,
                Date = m.Date,
                Message = m.Message,
                Username = m.Username
            }).ToList();
            
            return messagesDto;


        }
        public async Task<bool> CreateMessage(string message)
        {
            var signedInUser = await _signInManager.UserManager.GetUserAsync(_signInManager.Context.User);

            if (signedInUser == null)
            {
                throw new Exception("User must be signed in to create a message.");
            }
            var username = signedInUser.UserName;

            bool result = await _messageRepo.CreateMessageAsync(message, username);

            return result;

            //return new MessageDetailDto
            //{
            //    Date = DateTime.UtcNow,
            //    Message = message,
            //    Username = username
            //};

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
