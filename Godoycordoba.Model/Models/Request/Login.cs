using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Godoycordoba.Model.Models.Request
{
    public class Login
    {
        [JsonProperty("Username")]
        public string? Username { get; set; }
        [JsonProperty("Password")]
        public string? Password { get; set; }
    }
}
