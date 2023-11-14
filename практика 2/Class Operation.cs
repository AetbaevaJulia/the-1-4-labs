using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace практика_2
{
    internal class Operation : Token
    {

        public int Priority(char symbol)
        {
            if (symbol == '(' || symbol == ')') return 0;
            else if (symbol == '+' || symbol == '-') return 1;
            else return 2;
        }
    }
}
