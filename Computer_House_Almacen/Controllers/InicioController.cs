using Computer_House_Almacen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Computer_House_Almacen.Models;
using Computer_House_Almacen.Recursos;
using Computer_House_Almacen.Servicios.Contrato;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Computer_House_Almacen.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public InicioController(IUsuarioService usuarioService) { 
            _usuarioService = usuarioService;
        }
        public IActionResult InicioSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InicioSesion(string usuario, string contrasenia)
        {
            Usuario usuario_encontrado = await _usuarioService.GetUsuarioById(usuario, Utilidades.EncriptarClave(contrasenia));
            if(usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.Usuario1)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("InicioSesion","Inicio");

        }
    }
}
