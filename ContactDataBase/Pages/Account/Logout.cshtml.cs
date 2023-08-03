using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDataBase.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Account/SignIn");
        }
    }
}
