using ChalkboardChat.BLL.DTOs.MessageDtos;
using ChalkboardChat.BLL.Interfaces;
using ChalkboardChat.BLL.Services;
using ChalkboardChat.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ChalkboardChat.UI.Pages.Member
{
    public class MessagesModel : PageModel
    {
        private readonly MessageService _messageService;

        public MessagesModel(MessageService messageService)
        {
            _messageService = messageService;
        }

        [BindProperty]
        public string NewMessage { get; set; }

        public List<MessageListDto> Messages { get; set; }

        public async Task OnGet()
        {
            Messages = await _messageService.GetAllMessages();
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrWhiteSpace(NewMessage))
            {
                return Page();
            }

            bool result = await _messageService.CreateMessage(NewMessage);

            if (result)
            {
                
            }
            return RedirectToPage();
        }
    }
}
