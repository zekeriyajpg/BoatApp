using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Business.DataProtection
{
    public class DataProtection : IDataProtection
    {
        private readonly IDataProtector _protector;


        public DataProtection(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("Boatapp-security-v1");
        }

        public string Cripted(string text)
        {
            return _protector.Protect(text);
        }

        public string UnCripted(string criptedText)
        {
            
            
                return _protector.Unprotect(criptedText);
                
            
            
        }
    }
}
