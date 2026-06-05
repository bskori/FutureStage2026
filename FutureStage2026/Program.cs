using FutureStage2026.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddSession();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();


app.UseAuthorization();


app.UseRouting();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Parent}/{action=Login}/{id?}");



app.Run();
