using Computer_House_Almacen.Models;
using Microsoft.EntityFrameworkCore;

using DinkToPdf;
using DinkToPdf.Contracts;
using Computer_House_Almacen.Extension;
using Microsoft.AspNetCore.Authentication.Cookies;
using Computer_House_Almacen.Servicios.Contrato;
using Computer_House_Almacen.Servicios.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbComputerHouseAlmacenContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

var context = new CustomAssamblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "LibreriaPDF/libwkhtmltox.dll"));
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


builder.Services.AddScoped<IUsuarioService,UsuarioService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Inicio/InicioSesion";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});

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
    pattern: "{controller=Inicio}/{action=InicioSesion}/{id?}");

app.Run();
