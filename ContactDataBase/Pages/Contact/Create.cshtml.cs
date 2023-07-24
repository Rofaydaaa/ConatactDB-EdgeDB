using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using EdgeDB;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContactDataBase.Pages.Contact
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ContactInfoInput ContactInfoInput { get; set; }
        public string SuccessMessage { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
        private readonly EdgeDBClient _client;

        public CreateModel(EdgeDBClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Data validation failed";
                return Page();
            }

            string formattedDateOfBirth = ContactInfoInput.DateOfBirth.ToString("yyyy-MM-dd");
            await _client.ExecuteAsync($@"
				INSERT ContactInfo {{
					first_name := ""{ContactInfoInput.FirstName}"",
					last_name := ""{ContactInfoInput.LastName}"",
					email := ""{ContactInfoInput.Email}"",
					title := ""{ContactInfoInput.Title}"",
					description := ""{ContactInfoInput.Description}"",
				    date_birth := cal::to_local_date(""{formattedDateOfBirth}""),
					marriage_status := {ContactInfoInput.MarriageStatus}
				}}
			");

            SuccessMessage = "Contact is added successfully";
            TempData["SuccessMessage"] = SuccessMessage;
            return RedirectToPage("Display");
        }
    }

    public class ContactInfoInput
    {
        [BindNever]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The first name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The last name is required.")]
        public string LastName { get; set; }

        [CustomEmailFormat(ErrorMessage = "Invalid email format. Please enter a valid email address.")]
        public string Email { get; set; }

        public Title Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool MarriageStatus { get; set; }

        public static ContactInfoInput FromContactInfo(ContactInfo contactInfo)
        {
            return new ContactInfoInput
            {
                Id = contactInfo.Id,
                FirstName = contactInfo.FirstName,
                LastName = contactInfo.LastName,
                Email = contactInfo.Email,
                Title = contactInfo.Title,
                Description = contactInfo.Description,
                DateOfBirth = new DateTime(contactInfo.DateBirth.DateOnly.Year, contactInfo.DateBirth.DateOnly.Month, contactInfo.DateBirth.DateOnly.Day),
                MarriageStatus = contactInfo.MarriageStatus
            };
        }
    }

    public class CustomEmailFormatAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string email = value.ToString();
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, emailPattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return "Invalid email format. Please enter a valid email address.";
        }
    }
}
