using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class OprDetEntradum
{
    public Guid CodigoDetEntrada { get; set; }

    public long Folio { get; set; }

    public int IdDepartamento { get; set; }

    public int IdPersonal { get; set; }

    public string? Observaciones { get; set; }

    public DateTime Fecha { get; set; }

    public virtual OprEntradum FolioNavigation { get; set; } = null!;

    public virtual CatDepartamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual SysUsuario IdPersonalNavigation { get; set; } = null!;
}
