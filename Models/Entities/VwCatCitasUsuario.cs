using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class VwCatCitasUsuario
{
    public Guid CodigoCita { get; set; }

    public DateTime Inicio { get; set; }

    public DateTime Fin { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? FolioAsociado { get; set; }

    public int UsuarioCreacion { get; set; }

    public bool Inactivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Usuario { get; set; } = null!;

    public int UsuarioId { get; set; }

    public int DepartamentoId { get; set; }
}
