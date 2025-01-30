using Godoycordoba.Model.Models.Modell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godoycordoba.Model.Models.Response
{
    public class Response
    {
        public int Status { get; set; }
        public List<Usuario>? Desciption { get; set; }
    }
}
