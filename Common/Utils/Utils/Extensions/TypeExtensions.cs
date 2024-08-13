using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class TypeExtensions
    {
        public static bool SkipValidation(this Type t, List<Type> typesForNotValidating)
        {
            var booleans = new List<bool>();
            foreach (Type type in typesForNotValidating)
            {
                booleans.Add(t.IsAssignableFrom(type));
            }
            var result = booleans.Aggregate((b1, b2) => b1 || b2);
            return result;
        }
    }
}
