using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;


namespace практика_2
{
    internal class Operation : Token
    {
        public  char Symbol { get; set; }
        public  int Priority { get; set; }
        
        public Operation(char symbol) 
        {
            Symbol = symbol;
            Priority = GetPriority(Symbol);
        }

        private static int GetPriority(char symbol)
        {
            if (symbol == '+' || symbol == '-') return 1;
            else return 2;
        }
    }
}
