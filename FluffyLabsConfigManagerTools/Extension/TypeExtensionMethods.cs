using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools.Extension
{
    public static class TypeExtensionMethods
    {
        public static string FloatToString<T>(this T number)
            where T : IConvertible
        {
            if(typeof(T) == typeof(float)
                || typeof(T) == typeof(double)
                || typeof(T) == typeof(decimal))
            {
                return number.ToString().AppendZero();
            }
            else
            {
                return number.ToString();
            }
        }
    }
}
