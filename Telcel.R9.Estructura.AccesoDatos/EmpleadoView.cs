using System;
using System.Collections.Generic;

namespace Telcel.R9.Estructura.AccesoDatos;

public partial class EmpleadoView
{
    public int EmpleadoId { get; set; }

    public string? Nombre { get; set; }

    public int? PuestoId { get; set; }

    public string? DescripcionPuesto { get; set; }

    public int? DepartamentoId { get; set; }

    public string? DescripcionDepartamento { get; set; }
}
