using System;
using System.Collections.Generic;

namespace Godoycordoba.Data.Context;

public partial class TiposDocumento
{
    public int IdTipoDocumento { get; set; }

    public string Documento { get; set; } = null!;
}
