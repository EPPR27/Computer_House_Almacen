using Microsoft.EntityFrameworkCore;
using Computer_House_Almacen.Models;
using Computer_House_Almacen.Servicios.Contrato;

namespace Computer_House_Almacen.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DbComputerHouseAlmacenContext _context;
        public UsuarioService (DbComputerHouseAlmacenContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioById(string usuario, string contrasenia)
        {
            Usuario usuario_encontrado = await _context.Usuarios.Where(u => u.Usuario1 == usuario && u.Contrasenia == contrasenia)
                .FirstOrDefaultAsync();
            return usuario_encontrado;
        }
    }
}
