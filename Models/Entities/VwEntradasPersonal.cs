using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class VwEntradasPersonal
{
    public long Folio { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string? Asunto { get; set; }

    public string? OficinaOrigen { get; set; }

    public int IdTipoOrigen { get; set; }

    public string TipoOrigen { get; set; } = null!;

    public int? IdTipoEntrada { get; set; }

    public string? TipoEntrada { get; set; }

    public string ReferenciaRegistro { get; set; } = null!;

    public string? NumeroOficioRegistro { get; set; }

    public DateTime? FechaOficioRegistro { get; set; }

    public DateTime UltimaActualizacion { get; set; }

    public int DocumentosAdjuntos { get; set; }

    public int EventosRegistrados { get; set; }

    public int UsuarioId { get; set; }

    public string UsuarioNombre { get; set; } = null!;

    public int UsuarioIdDepo { get; set; }

    public string? UsuarioDepo { get; set; }

    public int UsuarioJerarquia { get; set; }
}
