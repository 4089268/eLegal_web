using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class CatTipoEntradum
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool? Inactivo { get; set; }

    public virtual ICollection<OprEntradum> OprEntrada { get; set; } = new List<OprEntradum>();
}
