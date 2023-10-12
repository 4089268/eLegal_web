using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class CatJerarquia
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<SysUsuario> SysUsuarios { get; set; } = new List<SysUsuario>();
}
