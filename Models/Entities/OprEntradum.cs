using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class OprEntradum
{
    public long Folio { get; set; }

    public DateTime Fecha { get; set; }

    public int IdUsuario { get; set; }

    public int IdOrigen { get; set; }

    public string? ReferenciaOrigen { get; set; }

    public string? Asunto { get; set; }

    public string? OficinaOrigen { get; set; }

    public string? NumOficio { get; set; }

    public DateTime? FechaOficio { get; set; }

    public DateTime? FechaConclucion { get; set; }

    public int? TipoEntrada { get; set; }

    public int EstatusId { get; set; }

    public virtual ICollection<CatCita> CatCita { get; set; } = new List<CatCita>();

    public virtual CatEstatus Estatus { get; set; } = null!;

    public virtual CatOrigen IdOrigenNavigation { get; set; } = null!;

    public virtual SysUsuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<OprDetEntradum> OprDetEntrada { get; set; } = new List<OprDetEntradum>();

    public virtual ICollection<OprEntradasDepartamento> OprEntradasDepartamentos { get; set; } = new List<OprEntradasDepartamento>();

    public virtual ICollection<OprEntradasUsuario> OprEntradasUsuarios { get; set; } = new List<OprEntradasUsuario>();

    public virtual ICollection<OprMedium> OprMedia { get; set; } = new List<OprMedium>();

    public virtual CatTipoEntradum? TipoEntradaNavigation { get; set; }
}
