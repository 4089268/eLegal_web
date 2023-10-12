using System;
using System.Collections.Generic;

namespace eLegal.Entities;

public partial class OprMedium
{
    public long? Folio { get; set; }

    public DateTime? Fecha { get; set; }

    public byte[]? Archivo { get; set; }

    public string? Tipo { get; set; }

    public string? Observacion { get; set; }

    public Guid CodigoDetEntrada { get; set; }

    public Guid CodigoDocumento { get; set; }

    public virtual OprEntradum? FolioNavigation { get; set; }
}
