using EdgeDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEdgeDB(EdgeDBConnection.FromInstanceName("contact-app"), config =>
{
	config.SchemaNamingStrategy = INamingStrategy.SnakeCaseNamingStrategy;
});
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

public record ContactInfo(Guid Id, string FirstName, string LastName, string Email, Title Title, string Description, EdgeDB.DataTypes.LocalDate DateBirth, bool MarriageStatus);