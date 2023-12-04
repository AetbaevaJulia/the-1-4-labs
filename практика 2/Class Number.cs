using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace практика_2
{
    internal class Number : Token
    {
        public double Value;
        public Number(double num) 
        {
            Value = num;
        }
        public static bool IsDigitNumber(char symbol) 
        {
            string str = "0123456789";
            if (str.Contains(symbol)) return true;
            return false;
        }
    }
}
