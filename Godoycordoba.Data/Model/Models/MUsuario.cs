using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godoycordoba.Data.Model.Models
{
    public class MUsuario
    {
        public int Id { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public int IdTipoDocumento { get; set; }

        public string? Documento { get; set; }

        public string? CorreoElentronico { get; set; }

        public DateTime FechaIngreso { get; set; }
        public int Puntaje { get; set; }
    }
}
