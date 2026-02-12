using ChalkboardChat.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.DAL.DATAs
{
    public interface IMessageRepository
    {
        Task<List<MessageModel>> GetAllMessagesAsync();
        Task<bool> CreateMessageAsync(string messageText, string username);
    }
}
