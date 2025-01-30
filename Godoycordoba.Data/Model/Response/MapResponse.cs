using Godoycordoba.Data.Context;
using Godoycordoba.Data.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godoycordoba.Data.Model.Response
{
    public class MapResponse
    {
        public static ResponseUsuario ResponseMessage(int status, object usuarios, string statusDescription, bool response) 
        {
            if(response)
            {
                return new ResponseUsuario
                {
                    Response = new Response
                    {
                        Status = 200,
                        Desciption = null,
                        StatusDescription = statusDescription

                    },
                    Usuario = usuarios
                };
            }
            else
            {
                return new ResponseUsuario
                {
                    Response = new Response
                    {
                        Status = 500,
                        Desciption = null,
                        StatusDescription = statusDescription

                    }
                };
            }
        }
    }
}
