using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class CatCita
{
    public Guid CodigoCita { get; set; }

    public DateTime Inicio { get; set; }

    public DateTime Fin { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public long? FolioAsociado { get; set; }

    public int? UsuarioCreacion { get; set; }

    public bool Inactivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual OprEntradum? FolioAsociadoNavigation { get; set; }

    public virtual SysUsuario? UsuarioCreacionNavigation { get; set; }
}
