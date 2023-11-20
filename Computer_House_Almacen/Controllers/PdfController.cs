using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Extensions;

using DinkToPdf.Contracts;
using Computer_House_Almacen.Models;
using DinkToPdf;

namespace Computer_House_Almacen.Controllers
{
    public class PdfController : Controller
    {
        private readonly IConverter _converter;
        private readonly DbComputerHouseAlmacenContext _context;
        public PdfController(IConverter converter, DbComputerHouseAlmacenContext context) 
        {
            _context = context;
            _converter = converter;
        }
        public IActionResult VistaParaPdf()
        {
            List<Producto> lista = _context.Productos.Include(m => m.oMarca).Include(c => c.oCategoria).ToList();
            return View(lista);
        }
        public IActionResult DescargarPdf()
        {
            string pagina_Actual = HttpContext.Request.Path;
            string url_Pagina = HttpContext.Request.GetEncodedUrl();
            url_Pagina = url_Pagina.Replace(pagina_Actual, "");
            url_Pagina = $"{url_Pagina}/Pdf/VistaParaPdf";

            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        Page = url_Pagina
                    }
                }
            };
            var archivoPDF = _converter.Convert(pdf);
            string nombrePDF = "reporte_Almacen_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            return File(archivoPDF, "application/pdf", nombrePDF);
        }
    }
}
