using SpaceAdventures.MVC.Configurations;
using SpaceAdventures.MVC.Policies;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;


// Add services to the container.

// Authentication Service
builder.Services.AddAuthenticationServiceCollection(configuration);

// SameSiteNoneCookie Service
builder.Services.AddSameSiteNoneCookiesServiceCollection();


builder.Services.AddControllersWithViews();

// Policy Service

builder.Services.AddSingleton<ClientPolicy>(new ClientPolicy());

builder.Services.AddHttpClient("RetryPolicy").AddPolicyHandler(
    request => new ClientPolicy().ExponentialHttpRetry);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
