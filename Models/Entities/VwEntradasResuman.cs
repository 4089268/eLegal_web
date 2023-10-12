using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class VwEntradasResuman
{
    public long Folio { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int IdPersonalRegistro { get; set; }

    public string? PersonalRegistro { get; set; }

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

    public int EstatusId { get; set; }

    public string? Estatus { get; set; }

    public string? IdDepartamentoAsignados { get; set; }

    public string? DepartamentoAsignados { get; set; }

    public string? IdPersonalAsignados { get; set; }

    public string? PersonalAsignados { get; set; }

    public int DocumentosAdjuntos { get; set; }

    public int EventosRegistrados { get; set; }
}
