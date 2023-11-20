using System;
using System.Collections.Generic;

namespace Computer_House_Almacen.Models;

public partial class Producto
{
    public int Idprod { get; set; }

    public string Nomprod { get; set; } = null!;

    public int Idcate { get; set; }

    public int Idmarca { get; set; }

    public decimal Precio { get; set; }

    public int? Stock { get; set; }

    public string? Estado { get; set; }

    public virtual Categoria oCategoria { get; set; } = null!;

    public virtual Marca oMarca { get; set; } = null!;
}
