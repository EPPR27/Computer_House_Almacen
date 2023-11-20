using Computer_House_Almacen.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Computer_House_Almacen.Models.ViewModel
{
    public class ProductoVM
    {
        public Producto oProducto { get; set; }
        public List<SelectListItem> oListaMarca { get; set; }
        public List<SelectListItem> oListaCategoria { get; set; }
    }
}
