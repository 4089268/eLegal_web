using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class CatDepartamento
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<OprDetEntradum> OprDetEntrada { get; set; } = new List<OprDetEntradum>();

    public virtual ICollection<OprEntradasDepartamento> OprEntradasDepartamentos { get; set; } = new List<OprEntradasDepartamento>();

    public virtual ICollection<SysUsuario> SysUsuarios { get; set; } = new List<SysUsuario>();
}
