using Godoycordoba.Data.Context;
using Godoycordoba.Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Godoycordoba.Logic.Repository
{
    public class UpdateLoginDate(GodoycordobaContext context) : ILogic
    {
        private readonly GodoycordobaContext _context = context;

        async Task<bool> ILogic.UpdateLoginDate(Usuario usuario)
        {
            var item = _context.Usuarios.FirstOrDefault(x => x.Documento.Equals(usuario.Documento));

            if (item != null && await Update(item))
                return true;
            return false;
        }
        private async Task<bool> Update(Usuario item)
        {

            item.Documento = item.Documento;
            item.FechaIngreso = DateTime.Now;
            if (await _context.SaveChangesAsync() == 0)
                return true;
            return false;
        }

        string ILogic.Classify(DateTime? date)
        {
            string cln = "";
            var tiempo = (DateTime.Now - date).Value.TotalHours;
            if (tiempo < 12)
                cln = "Hechicero";
            if (tiempo > 12 && tiempo < 48)
                cln = "Luchador";
            if (tiempo > 48 && tiempo < 168)
                cln = "Explorador";
            if (tiempo > 168)
                cln = "Olvidado";

            return cln;
        }

        public int SetScore(string nombre, string apellido)
        {
            int nombreLength = nombre.Length + apellido.Length;
            int score = 0;
            if (nombreLength > 10)
                score = 20;
            if (nombreLength > 5 && nombreLength < 10)
                score = 10;
            if (nombreLength < 5)
                score = 0;

            return score;
        }

        public int EmailScore(string email)
        {
            int score = 0;
            if (email.Contains("gmail.com"))
                score = 40;
            else if (email.Contains("hotmail.com"))
                score = 20;
            else
                score = 10;

            return score;
        }
    }
}
