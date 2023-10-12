using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class SysUsuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Mail { get; set; }

    public string Usuario { get; set; } = null!;

    public string? Password { get; set; }

    public decimal IdEmpleado { get; set; }

    public DateTime Caducidad { get; set; }

    public bool Inactivo { get; set; }

    public int IdJerarquia { get; set; }

    public int? IdDepartamento { get; set; }

    public virtual ICollection<CatCita> CatCita { get; set; } = new List<CatCita>();

    public virtual CatDepartamento? IdDepartamentoNavigation { get; set; }

    public virtual CatJerarquia IdJerarquiaNavigation { get; set; } = null!;

    public virtual ICollection<OprDetEntradum> OprDetEntrada { get; set; } = new List<OprDetEntradum>();

    public virtual ICollection<OprEntradum> OprEntrada { get; set; } = new List<OprEntradum>();

    public virtual ICollection<OprEntradasUsuario> OprEntradasUsuarios { get; set; } = new List<OprEntradasUsuario>();
}
