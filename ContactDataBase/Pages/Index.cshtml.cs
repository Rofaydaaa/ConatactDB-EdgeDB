using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDataBase.Pages
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
			return RedirectToPage("/Contact/Display");
		}
    }
}