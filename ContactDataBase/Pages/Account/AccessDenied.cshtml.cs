using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDataBase.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
			TempData["ErrorMessage"] = "You don't have acccess for the following action, only Admin are allowed";
			return RedirectToPage("/Contact/Display");
		}
    }
}
