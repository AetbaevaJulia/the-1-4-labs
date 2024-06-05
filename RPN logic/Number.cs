using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN_logic
{
    class Number : Token
    {
        public double Value;
        public Number(string num)
        {
            Value = Convert.ToDouble(num);
        }
        public Number(double value)
        {
            Value = value;
        }

        public static bool IsDigitNumber(char symbol)
        {
            string str = "0123456789";
            if (str.Contains(symbol)) return true;
            return false;
        }

    }
}
