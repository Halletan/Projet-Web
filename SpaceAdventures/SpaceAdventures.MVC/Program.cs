
using SpaceAdventures.MVC.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;



/******  The Service's Container ******/

// Authentication Service
builder.Services.AddAuthenticationServiceCollection(configuration);

// SameSiteNoneCookie Service
builder.Services.AddSameSiteNoneCookiesServiceCollection();


// Services
builder.Services.AddServiceCollection();

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Session Variables - Views based on roles
app.UseSession();

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();