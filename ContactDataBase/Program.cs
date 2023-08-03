using ContactDataBase.Pages.Contact;
using EdgeDB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEdgeDB(EdgeDBConnection.FromInstanceName("contact-app"), config =>
{
	config.SchemaNamingStrategy = INamingStrategy.SnakeCaseNamingStrategy;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/SignIn";
		options.AccessDeniedPath = "/Account/AccessDenied";
	});
builder.Services.AddScoped<IPasswordHasher<ContactInfoInput>, PasswordHasher<ContactInfoInput>>();
builder.Services.AddScoped<ContactDataBase.Query>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

public enum Title
{
	Mr,
	Mrs,
	Miss,
	Dr,
	Prof,
}

public enum RoleUser
{
    Admin,
	Normal
}

public record ContactInfo(
	Guid Id, 
	string FirstName, 
	string LastName, 
	string Email, 
	string Username,
	string Password,
	Title Title, 
	string Description,
	EdgeDB.DataTypes.LocalDate DateBirth,
	bool MarriageStatus,
	RoleUser RoleUser);