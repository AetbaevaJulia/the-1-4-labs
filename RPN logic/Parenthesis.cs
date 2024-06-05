using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN_logic
{
    class Parenthesis : Token
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
        public static bool IsParenthesis(char symbol)
        {
            if (symbol == '(' || symbol == ')')
            {
                return true;
            }
            return false;
        }
    }
}
