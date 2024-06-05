using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN_logic
{
    class Variable : Token
    {
        public string Name;
        public Variable(string name)
        {
            Name = name;
        }

        public static bool IsVariable(string symbol)
        {
            symbol = symbol.ToUpper();
            if (symbol == "Х" || symbol == "X")
            {
                return true;
            }
            return false;
        }
    }
}
