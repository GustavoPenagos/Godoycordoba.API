using Godoycordoba.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godoycordoba.Logic.Interfaces
{
    public interface ILogic
    {
        Task<bool> UpdateLoginDate(Usuario documento);
        string Classify(DateTime? date);
        int SetScore(string nombre, string apellido);
        int EmailScore(string email);
    }
}
