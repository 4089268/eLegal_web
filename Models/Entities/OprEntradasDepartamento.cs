using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class OprEntradasDepartamento
{
    public int Id { get; set; }

    public long Folio { get; set; }

    public int IdDepartamento { get; set; }

    public virtual OprEntradum FolioNavigation { get; set; } = null!;

    public virtual CatDepartamento IdDepartamentoNavigation { get; set; } = null!;
}
