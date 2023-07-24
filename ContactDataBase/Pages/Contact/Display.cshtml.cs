using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDataBase.Pages.Contact
{
    public class DisplayModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public List<ContactInfo> ContactInfoList { get; set; } = new List<ContactInfo>();
        private readonly EdgeDBClient _client;

        public DisplayModel(EdgeDBClient client)
        {
            _client = client;
        }

        public async Task OnGet()
        {
            await GetContactList();
        }

        public async Task<PartialViewResult> OnGetSearch(string searchTerm)
        {
            await GetContactList();

            if (string.IsNullOrEmpty(searchTerm))
            {
                return Partial("_ContactTable", ContactInfoList);
            }
            else
            {
                var filteredList = ContactInfoList.Where(item =>
                    item.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                    || item.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                return Partial("_ContactTable", filteredList);
            }
        }

        public async Task GetContactList()
        {
            ContactInfoList = (await _client.QueryAsync<ContactInfo>(@"
                SELECT ContactInfo {
                    first_name,
                    last_name,
                    email,
                    title,
                    description,
                    date_birth,
                    marriage_status
                }
                ORDER BY .first_name
            ")).ToList();
        }
    }
}
