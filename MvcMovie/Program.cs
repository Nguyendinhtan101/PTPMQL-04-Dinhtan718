using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using OfficeOpenXml; // nhớ dùng EPPlus
using Microsoft.Extensions.DependencyInjection;
using MvcMovie;
using  MvcMovie.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models.Process;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();
        var mailSettings = builder.Configuration.GetSection("MailSettings");
        builder.Services.Configure<MailSettings>(mailSettings);
        builder.Services.AddTransient<IEmailSender, SendMailService>();

 //builder.Services.AddDbContext<DataApplicationDbContext>(options =>
    //options.UseSqlite(builder.Configuration.GetConnectionString("DataApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'DataApplicationDbContext' not found.")));
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
        
        
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    //Config Password
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
     //Config Login
     options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    //Config User
    options.User.RequireUniqueEmail = true;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    //chi gui Cooke qua HTTPS
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //Giam thieu rui ro CSRF
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});
// Cấu hình DbContext
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddControllersWithViews();


// Thêm dòng này để cấu hình Authorization
builder.Services.AddAuthorization();

// Thêm các dịch vụ MVC
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



builder.Services.AddTransient<EmployeeSeeder>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var Services = scope.ServiceProvider;
    var seeder = Services.GetRequiredService<EmployeeSeeder>();
    seeder.SeendEmployees(1000);
 }

// Pipeline xử lý HTTP
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();

app.UseRouting();
app.UseAuthentication();
// Cho phép sử dụng Authorization
app.UseAuthorization();

// Route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
