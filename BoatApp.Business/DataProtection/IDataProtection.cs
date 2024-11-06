using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Business.DataProtection
{
    public interface IDataProtection
    {
        string Cripted(string text);
        string UnCripted(string criptedText);

    }
}
