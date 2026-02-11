using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChalkboardChat.BLL.DTOs.MessageDtos
{
    internal class CreateMessageDto
    {
        //public int Id { get; set; } behövs inte för att databasen skapar det åt oss
        public DateTime Date { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
