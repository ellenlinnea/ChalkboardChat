using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        [Required(ErrorMessage = "Användarnamn krävs")]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Lösenord krävs")]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Implementera inloggningslogik här
            var result = _signInManager.PasswordSignInAsync(Username, Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Result.Succeeded)
            {
                return RedirectToPage("/Member/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ogiltigt användarnamn eller lösenord.");
                return Page();
            }
        }
    }
}
