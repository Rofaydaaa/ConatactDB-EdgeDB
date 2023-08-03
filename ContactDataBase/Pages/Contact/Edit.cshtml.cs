using EdgeDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace ContactDataBase.Pages.Contact
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty]
        public ContactInfoInput ContactInfoInput { get; set; }
        public string SuccessMessage { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
        private readonly Query _query;

        public EditModel(Query query)
        {
            _query = query;
        }
        public async Task OnGet()
        {
            ContactInfoInput = await _query.GetContactWithId(Request.Query["Id"]);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Data validation failed";
                return Page();
            }
            bool isUsernameTaken = await _query.IsUsernameTaken(ContactInfoInput.Username);
            var previousContactInfo = await _query.GetContactWithId(Request.Query["Id"]);
            if (previousContactInfo.Username != ContactInfoInput.Username && isUsernameTaken)
            {
                ErrorMessage = "Username already Taken";
                return Page();
            }
            await _query.UpdateContactInfoInputWithId(Request.Query["Id"], ContactInfoInput);
            SuccessMessage = "Contact is updated successfully";
            TempData["SuccessMessage"] = SuccessMessage;
            return RedirectToPage("Display");
        }
    }
}
