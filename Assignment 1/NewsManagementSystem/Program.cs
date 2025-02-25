using Microsoft.EntityFrameworkCore;
using NewsManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FunewsManagementContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("FunewsManagementDB")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
