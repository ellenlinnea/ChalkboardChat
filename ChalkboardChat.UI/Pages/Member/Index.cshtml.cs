using ChalkboardChat.BLL.DTOs.MessageDtos;
using ChalkboardChat.BLL.Interfaces;
using ChalkboardChat.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ChalkboardChat.UI.Pages.Member
{
    public class MessagesModel : PageModel
    {
        private readonly IMessageService _messageService;

        public MessagesModel(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [BindProperty]
        public string NewMessage { get; set; }

        public List<MessageListDto> Messages { get; set; }

        public void OnGet()
        {
            // Hämtar alla meddelande vid laddning av sidan
            Messages = _messageService.GetAllMessages();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(NewMessage))
            {
                return Page();
            }

            -_messageService.CreateMessage(NewMessage)

            return RedirectToPage();
        }
    }
}
