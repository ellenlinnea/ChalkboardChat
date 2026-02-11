using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChalkboardChat.BLL.DTOs.MessageDtos
{
    public class MessageListDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Username { get; set; } 
    }
}
