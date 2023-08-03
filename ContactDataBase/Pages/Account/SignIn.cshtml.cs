using ContactDataBase.Pages.Contact;
using EdgeDB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ContactDataBase.Pages.Account
{
    [BindProperties]
    public class SignInModel : PageModel
    {
        private readonly Query _query;
        private readonly IPasswordHasher<ContactInfoInput> _passwordHasher;
        public SignInInput SignInInput { get; set; }

        public SignInModel(Query query, IPasswordHasher<ContactInfoInput> passwordHasher)
        {
            _query = query;
            _passwordHasher = passwordHasher;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostSignin()
        {
            if (ModelState.IsValid)
            {
                ContactInfoInput? user = await AuthenticateUser(SignInInput);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, SignInInput.Username),
                        new Claim(ClaimTypes.Role, user.RoleUser.ToString()),
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return RedirectToPage("/Contact/Display");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                }
            }
            return Page();
        }

        public async Task<ContactInfoInput?> AuthenticateUser(SignInInput signInInput)
        {
            ContactInfo? currentContactInfo = await _query.GetContactWithUsername(signInInput.Username);
            if (currentContactInfo != null)
            {
                ContactInfoInput contactInfoInput = ContactInfoInput.FromContactInfo(currentContactInfo);
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(contactInfoInput, currentContactInfo.Password, signInInput.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    return contactInfoInput;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }

    public class SignInInput
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
