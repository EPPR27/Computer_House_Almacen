using System;
using System.Collections.Generic;

namespace Computer_House_Almacen.Models;

public partial class Marca
{
    public int Idmarca { get; set; }

    public string Nommarca { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
