using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MessageDbContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("MessageConnection")));

builder.Services.AddDbContext<AuthDbContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection")));

//Behöver aktivera identity
//AddDefaultIdentity lägger till all standardfuntionalitet för identity, som inloggning, registrering, lösenordshantering osv
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<IdentityDbContext>();
//Sista säger: spara allt i vår databas IdentityDbContext

builder.Services.AddAuthorization(options =>
{
    //Policy är namnet på en regel - regeln är att användaren måste ha rollen "Admin" för att få tillgång
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login"; //Om användaren inte är inloggad, skicka dem till /Login-sidan
    options.AccessDeniedPath = "/AccessDenied"; //Om användaren inte har rätt behörighet, skicka dem till /AccessDenied-sidan
});


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Member"); //Krav på inloggning för alla sidor i Member-mappen
    options.Conventions.AuthorizeFolder("/Admin", "AdminOnly"); //Krav på inloggning och Admin-roll för alla sidor i Admin-mappen
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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
