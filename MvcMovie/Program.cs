using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using OfficeOpenXml; // nhớ dùng EPPlus
using Microsoft.Extensions.DependencyInjection;
using MvcMovie;
using  MvcMovie.Models ;
using Microsoft.AspNetCore.Identity;
//using VicemMVCIdentity.Data;
//using VicemMVCIdentity.Data;
var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<DataApplicationDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DataApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'DataApplicationDbContext' not found.")));
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Cấu hình DbContext
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddControllersWithViews();


// Thêm dòng này để cấu hình Authorization
builder.Services.AddAuthorization();

// Thêm các dịch vụ MVC
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
