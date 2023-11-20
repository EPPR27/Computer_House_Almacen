using System;
using System.Collections.Generic;

namespace Computer_House_Almacen.Models;

public partial class Categoria
{
    public int Idcate { get; set; }

    public string Nomcate { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
