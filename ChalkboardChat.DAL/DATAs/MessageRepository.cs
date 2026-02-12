using ChalkboardChat.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ChalkboardChat.DAL.DATAs
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageDbContext _messageDbContext; //Hämtar databasen

        public MessageRepository(MessageDbContext messageDbContext) //Konstruktor
        {
            _messageDbContext = messageDbContext;
        }

        //METOD: Hämtar alla meddelanden
        public async Task<List<MessageModel>> GetAllMessagesAsync()
        {
            //Hämtar alla meddelanden, sorterar nyast först, omvandlar till lista.
            return await _messageDbContext.Messages
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        //METOD: Skapa meddelande
        public async Task CreateMessageAsync(string messageText, string username)
        {
            //Lägger in inputs, användarnamn samt aktuellt datum när meddelande skrivs
            var message = new MessageModel
            {
                Message = messageText,
                Username = username,
                Date = DateTime.Now
            };
            //Lägger till meddelandet i databasen
            await _messageDbContext.Messages.AddAsync(message);
            //Sparar meddelandet
            await _messageDbContext.SaveChangesAsync();
        }
    }
}