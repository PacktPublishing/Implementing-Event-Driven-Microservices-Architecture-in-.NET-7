using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowersOfTwo
{
    internal class Powers
    {
        public static System.Collections.Generic.IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }
    }
}
