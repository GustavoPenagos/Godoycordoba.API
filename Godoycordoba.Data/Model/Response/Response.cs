using Godoycordoba.Data.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godoycordoba.Data.Model.Response
{
    public class Response
    {
        public int Status { get; set; }
        public List<MUsuario>? Desciption { get; set; }
        public string? StatusDescription{ get; set; }
    }
}
