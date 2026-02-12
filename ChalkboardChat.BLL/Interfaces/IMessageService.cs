using ChalkboardChat.BLL.DTOs.MessageDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Interfaces
{
    //ETT INTERFACE ÄR KRAVET PÅ VILKA METODER SOM MÅSTE FINNAS I SERVICEN, man kan alltid lägga till fler metoder i servicen, men dessa MÅSTE vara med i servicen
    public interface IMessageService
    {
        //GET: metod att visa alla meddelande i listan:
        Task<List<MessageListDto>> GetAllMessages();

        //POST: create message
        Task<MessageDetailDto> CreateMessage(string message);
    }
}

//EF(4ST) OCH IDENTITY I DAL
//IDENTITY.UI I UI
