using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using FastFood.Services;
using FastFood.Repository.Data;
using FastFood.Repository.Repository.IRepository;
using FastFood.Repository.Repository;
using Newtonsoft.Json;
using Stripe;
using FastFood.Repository.DbInitializer;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = Environment.GetEnvironmentVariable("Facebook_APP_ID");
    options.AppSecret = Environment.GetEnvironmentVariable("Facebook_APP_SECRET");

});
builder.Services.AddAuthentication().AddMicrosoftAccount(options =>
{
    options.ClientId = Environment.GetEnvironmentVariable("MICROSOFT_CLIENT_ID");
    options.ClientSecret = Environment.GetEnvironmentVariable("MICROSOFT_CLIENT_SECRET");

});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IDbInitializer,DbInitializer>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender,EmailSender>();
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
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseRouting();

app.UseAuthorization();
app.UseSession();
SeadDataBase();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeadDataBase()
{
    using (var scope = app.Services.CreateScope())
    {
        var DdbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DdbInitializer.Initialize();
    }
}