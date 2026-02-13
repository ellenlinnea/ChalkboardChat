using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.UI.Pages.Member
{
    public class UpdateDetailsModel : PageModel
    {
        // Hanterar användare (skapa, ta bort, byta lösenord, etc.)
        private readonly UserManager<IdentityUser> _userManager;
        // Hanterar inloggning/utloggning
        private readonly SignInManager<IdentityUser> _signInManager;

        // Dependency Injection - hämtar UserManager och SignInManager från systemet
        public UpdateDetailsModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Formulärfält som binds till formuläret i .cshtml
        [BindProperty]
        [Required(ErrorMessage = "Nytt användarnamn krävs")]
        public string NewUsername { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Nuvarande lösenord krävs")]
        public string CurrentPassword { get; set; } = string.Empty;

        // Nullable (?) = valfritt, behöver inte fyllas i
        [BindProperty]
        public string? NewPassword { get; set; }

        // Jämför med NewPassword så att de matchar
        [BindProperty]
        [Compare("NewPassword", ErrorMessage = "Lösenorden matchar inte")]
        public string? ConfirmNewPassword { get; set; }

        // Meddelande som visas efter lyckad uppdatering (överlever en redirect)
        [TempData]
        public string? SuccessMessage { get; set; }

        // Körs när sidan laddas (GET-request)
        public async Task<IActionResult> OnGetAsync()
        {
            // Hämta inloggad användare från databasen
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }

            // Förifyller användarnamn-fältet med nuvarande namn
            NewUsername = user.UserName ?? string.Empty;
            return Page();
        }


        // Körs när "Spara ändringar"-knappen klickas (POST-request)
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            //Kontrollerar alla som är required och att NewPassword och ConfirmNewPassword matchar
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Hämta inloggad användare från databasen
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }

            // Verifiera att nuvarande lösenord stämmer
            var passwordCheck = await _userManager.CheckPasswordAsync(user, CurrentPassword);
            if (!passwordCheck)
            {
                ModelState.AddModelError("CurrentPassword", "Fel lösenord.");
                return Page();
            }

            // Byt användarnamn om det ändrats
            if (NewUsername != user.UserName)
            {
                var setUsernameResult = await _userManager.SetUserNameAsync(user, NewUsername);
                if (!setUsernameResult.Succeeded)
                {
                    // Visa felmeddelanden (t.ex. "användarnamnet är redan taget")
                    foreach (var error in setUsernameResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            // Kolla att båda lösenordsfälten är ifyllda om man vill byta lösenord
            if (!string.IsNullOrEmpty(NewPassword) || !string.IsNullOrEmpty(ConfirmNewPassword))
            {
                if (NewPassword != ConfirmNewPassword)
                {
                    ModelState.AddModelError("ConfirmNewPassword", "Lösenorden matchar inte.");
                    return Page();
                }
            }

            // Byt lösenord bara om användaren faktiskt skrev in ett nytt
            if (!string.IsNullOrEmpty(NewPassword))
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    // Visa felmeddelanden (t.ex. "lösenordet måste vara minst 6 tecken")
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            // Uppdatera inloggnings-cookien så användaren förblir inloggad
            await _signInManager.RefreshSignInAsync(user);
            SuccessMessage = "Dina uppgifter har uppdaterats!";
            return RedirectToPage();
        }

        // Körs när "Ta bort mitt konto"-knappen klickas
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            // Hämta inloggad användare från databasen
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }

            // Ta bort användaren från databasen
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                // Logga ut och skicka till startsidan
                await _signInManager.SignOutAsync();
                return RedirectToPage("/Index");
            }

            // Visa eventuella fel
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
