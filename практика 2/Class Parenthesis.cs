using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace практика_2
{
    internal class Parenthesis : Token
    {
        public char Symbol;
        public Parenthesis(char symbol) 
        {
           Symbol = symbol;
        }
        public static bool IsClosedParenthesis(char symbol)
        {
            if (symbol == ')') return true;
            return false;
        }
    }
}
