using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDataBase.Pages.Contact
{
    public class DeleteModel : PageModel
    {
        private readonly EdgeDBClient _client;
        public string SuccessMessage { get; set; } = "";

        public DeleteModel(EdgeDBClient client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnGet()
        {
            string id = Request.Query["Id"];
            await _client.QuerySingleAsync<ContactInfo>($@"
				delete ContactInfo
				filter .id = <uuid>""{id}""
			");
            return RedirectToPage("Display");
        }
    }
}
