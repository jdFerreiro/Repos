using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEAD.Helpers
{
    public static class SecurityHelper
    {
        public static string Encriptar(this string cadenaAEncriptar)
        {
            string result = string.Empty;
            byte[] encrypted = System.Text.Encoding.Unicode.GetBytes(cadenaAEncriptar);
            result = Convert.ToBase64String(encrypted);
            return result;
        }

        public static string Desencriptar(this string cadenaADesencriptar)
        {
            string result = string.Empty;
            byte[] decrypted = Convert.FromBase64String(cadenaADesencriptar);
            result = System.Text.Encoding.Unicode.GetString(decrypted, 0, decrypted.ToArray().Length);
            return result;
        }
    }
}