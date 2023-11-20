using Computer_House_Almacen.Models;
using Computer_House_Almacen.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace Computer_House_Almacen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbComputerHouseAlmacenContext _context;

        public HomeController(ILogger<HomeController> logger, DbComputerHouseAlmacenContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        { 
            List<Producto> lista = _context.Productos.Include(m => m.oMarca).Include(c => c.oCategoria).ToList();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Producto_Detalle(int idProducto)
        {
            ProductoVM oProductoVM = new ProductoVM()
            {
                oProducto = new Producto(),
                oListaCategoria = _context.Categoria.Select(categorias => new SelectListItem()
                {
                    Text = categorias.Nomcate,
                    Value = categorias.Idcate.ToString()
                }).ToList(),
                oListaMarca = _context.Marcas.Select(marcas => new SelectListItem()
                {
                    Text = marcas.Nommarca,
                    Value = marcas.Idmarca.ToString()
                }).ToList()
            };

            if (idProducto != 0)
            {
                oProductoVM.oProducto = _context.Productos.Find(idProducto);
            }
            return View(oProductoVM);
        }
        [HttpPost]
        public IActionResult Producto_Detalle(ProductoVM oProductoVM)
        {
            if (oProductoVM.oProducto.Idprod == 0)
            {
                _context.Productos.Add(oProductoVM.oProducto);
            }
            else
            {
                _context.Productos.Update(oProductoVM.oProducto);
            }
            if (oProductoVM.oProducto.Stock == 0 || oProductoVM.oProducto.Stock == null)
            {
                oProductoVM.oProducto.Estado = "AGOTADO";
            }
            else
            {
                oProductoVM.oProducto.Estado = "DISPONIBLE";
            }
            oProductoVM.oProducto.Stock = oProductoVM.oProducto.Stock ?? 0;
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Eliminar(int Idprod)
        {
            Producto oProducto = _context.Productos.Find(Idprod);
            _context.Productos.Remove(oProducto);
            _context.SaveChanges();
            return Json(new { success = true, message = "Eliminación exitosa" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}