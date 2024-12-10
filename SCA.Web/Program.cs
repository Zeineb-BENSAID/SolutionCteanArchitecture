using AM.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SCA.ApplicationCore.Interfaces;
using SCA.ApplicationCore.Services;
using SCA.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();//***********

//********************* DI ****************
//Injection des dépendances patterns

builder.Services.AddDbContext<DbContext, SCAContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<Type>(t => typeof(GenericRepository<>));

// injection des services

builder.Services.AddScoped<IProductService, ProductService>();



//*****************************************
var app = builder.Build();


app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.MapRazorPages();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
