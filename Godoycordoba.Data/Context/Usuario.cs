using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Godoycordoba.Data.Context;

public class Usuario
{
    public int Id { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public int IdTipoDocumento { get; set; }

    public string? Documento { get; set; } 

    public string? CorreoElentronico { get; set; }

    public DateTime? FechaIngreso { get; set; }
    [JsonIgnore]
    public int Puntaje { get; set; }
}
