using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using owasp1.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(configurer =>
{
    configurer.AddPolicy("just-owner", builder =>
    {
        builder.AddRequirements(new JustOwnerRequirement());
    });
});

builder.Services.AddHttpClient("some-httpclient")
   .ConfigurePrimaryHttpMessageHandler(() =>
   {
       return new HttpClientHandler()
       {
           AllowAutoRedirect = false
       };
   });


builder.Services.AddSingleton<IAuthorizationHandler, JustOwnerRequirementHandler>();

builder.Services.AddHashids(configurer =>
{
    configurer.Salt = "this_is_my_salt";
    configurer.MinHashLength = 8;
});


builder.Services.AddAuthentication(configurer =>
{
    configurer.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configurer.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddOpenIdConnect(configurer =>
{
    configurer.Authority = "https://demo.duendesoftware.com";
    configurer.ClientId = "interactive.public";
    configurer.ResponseType = "code";
    configurer.UsePkce = true;

    configurer.SaveTokens = true;
}).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
