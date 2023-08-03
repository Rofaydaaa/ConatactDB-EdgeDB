using EdgeDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace ContactDataBase.Pages.Contact
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Query _query;
        public string SuccessMessage { get; set; } = "";

        public DeleteModel(Query query)
        {
            _query = query;
        }
        public async Task<IActionResult> OnGet()
        {
            await _query.DeleteContactInfoInputWithId(Request.Query["Id"]);
            return RedirectToPage("Display");
        }
    }
}
