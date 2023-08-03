using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Authorization;

namespace ContactDataBase.Pages.Contact
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ContactInfoInput ContactInfoInput { get; set; }
        public string SuccessMessage { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
        private readonly Query _query;
        private readonly IPasswordHasher<ContactInfoInput> _passwordHasher;

        public CreateModel(Query query, IPasswordHasher<ContactInfoInput> passwordHasher)
        {
            _query = query;
            _passwordHasher = passwordHasher;
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Data validation failed";
                return Page();
            }
            bool isUsernameTaken = await _query.IsUsernameTaken(ContactInfoInput.Username);
            if (isUsernameTaken)
            {
                ErrorMessage = "Username already Taken";
                return Page();
            }
            ContactInfoInput.Password = _passwordHasher.HashPassword(ContactInfoInput, ContactInfoInput.Password);
            await _query.InsertContactInfoInput(ContactInfoInput);
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

        [Required(ErrorMessage = "The password is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        public string Password { get; set; }

        public Title Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool MarriageStatus { get; set; }
        public RoleUser RoleUser { get; set; }

        public static ContactInfoInput FromContactInfo(ContactInfo contactInfo)
        {
            return new ContactInfoInput
            {
                Id = contactInfo.Id,
                FirstName = contactInfo.FirstName,
                LastName = contactInfo.LastName,
                Email = contactInfo.Email,
                Username = contactInfo.Username,
                Password = contactInfo.Password,
                Title = contactInfo.Title,
                Description = contactInfo.Description,
                DateOfBirth = new DateTime(contactInfo.DateBirth.DateOnly.Year, contactInfo.DateBirth.DateOnly.Month, contactInfo.DateBirth.DateOnly.Day),
                MarriageStatus = contactInfo.MarriageStatus,
                RoleUser = contactInfo.RoleUser
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
