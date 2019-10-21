using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools.Extension
{
    internal static class StringExtensionMethods
    {       
        public static string AppendZero(this string s)
        {
            if (s.Contains("."))
            {
                return s;
            }
            else
            {
                return s + ".0";
            }
        }
    }
}
