using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class VwDetEntradum
{
    public long? Folio { get; set; }

    public Guid CodigoDetEntrada { get; set; }

    public DateTime Fecha { get; set; }

    public int IdDepartamento { get; set; }

    public string Departamento { get; set; } = null!;

    public int IdPersonal { get; set; }

    public string Personal { get; set; } = null!;

    public string? Observaciones { get; set; }
}
