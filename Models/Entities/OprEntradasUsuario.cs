using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class OprEntradasUsuario
{
    public int Id { get; set; }

    public long Folio { get; set; }

    public int IdUsuario { get; set; }

    public virtual OprEntradum FolioNavigation { get; set; } = null!;

    public virtual SysUsuario IdUsuarioNavigation { get; set; } = null!;
}
