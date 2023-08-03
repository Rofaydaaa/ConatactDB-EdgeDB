using EdgeDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDataBase.Pages.Contact
{
    [Authorize]
    public class DisplayModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public List<ContactInfo> ContactInfoList { get; set; } = new List<ContactInfo>();
        private readonly Query _query;

        public DisplayModel(Query query)
        {
            _query = query;
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
            ContactInfoList = await _query.GetAllContactList();
        }
    }
}
