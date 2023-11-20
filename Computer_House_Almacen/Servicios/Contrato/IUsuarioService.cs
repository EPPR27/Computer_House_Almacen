using Computer_House_Almacen.Models;
using Microsoft.EntityFrameworkCore;
namespace Computer_House_Almacen.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuarioById(string usuario, string contrasenia);
    }
}
