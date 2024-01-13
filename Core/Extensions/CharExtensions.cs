using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class CharExtensions
    {
        public static bool IsWhiteSpace(this char value)
        {
            return char.IsWhiteSpace(value);
        }
    }
}
