using System;
using System.Collections.Generic;

namespace Computer_House_Almacen.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;
}
