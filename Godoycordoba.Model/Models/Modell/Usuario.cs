using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godoycordoba.Model.Models.Modell
{
    public class Usuario
    {
        public int Id { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public int IdTipoDocumento { get; set; }

        public string Documento { get; set; } = null!;

        public string CorreoElentronico { get; set; } = null!;

        public DateOnly FechaIngreso { get; set; }
    }
}
