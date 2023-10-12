using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class CatOrigen
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<OprEntradum> OprEntrada { get; set; } = new List<OprEntradum>();
}
