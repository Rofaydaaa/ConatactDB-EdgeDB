using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDataBase.Pages.Contact
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ContactInfoInput ContactInfoInput { get; set; }
        public string SuccessMessage { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
        private readonly EdgeDBClient _client;

        public EditModel(EdgeDBClient client)
        {
            _client = client;
        }

        public async Task OnGet()
        {
            string id = Request.Query["Id"];
            ContactInfo currentContactInfo = await _client.QuerySingleAsync<ContactInfo>($@"
				SELECT ContactInfo {{
					first_name,
					last_name,
					email,
					title,
					description,
					date_birth,
					marriage_status
				}}
				filter .id = <uuid>""{id}""
				");

            ContactInfoInput = ContactInfoInput.FromContactInfo(currentContactInfo);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Data validation failed";
                return Page();
            }
            string id = Request.Query["Id"];
            string formattedDateOfBirth = ContactInfoInput.DateOfBirth.ToString("yyyy-MM-dd");
            await _client.ExecuteAsync($@"
				update ContactInfo 
				filter .id = <uuid>""{id}""
				set {{
					first_name := ""{ContactInfoInput.FirstName}"",
					last_name := ""{ContactInfoInput.LastName}"",
					email := ""{ContactInfoInput.Email}"",
					title := ""{ContactInfoInput.Title}"",
					description := ""{ContactInfoInput.Description}"",
				    date_birth := cal::to_local_date(""{formattedDateOfBirth}""),
					marriage_status := {ContactInfoInput.MarriageStatus}
				}}
			");

            SuccessMessage = "Contact is updated successfully";
            TempData["SuccessMessage"] = SuccessMessage;
            return RedirectToPage("Display");
        }
    }
}
