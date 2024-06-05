using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN_logic
{
    class CreatorToken
    {
        private static List<Operation> _operationsUses;

        public static Token Create(string buffer)
        {
            if (char.IsDigit(buffer[0]))
            {
                return new Number(buffer);
            }

            return CreateOperation(buffer);
        }

        public static Token Create(char symbol)
        {
            if (Variable.IsVariable(Convert.ToString(symbol)))
            {
                return new Variable(Convert.ToString(symbol));
            }

            if (symbol == '(' || symbol == ')')
            {
                return new Parenthesis(symbol);
            }

            return CreateOperation(symbol.ToString());
        }

        public static Operation CreateOperation(string name)
        {
            Operation operation = FindAvailableOperationByName(name);
            return operation;
        }

        private static Operation FindAvailableOperationByName(string name)
        {
            if (_operationsUses == null)
            {
                var parent = typeof(Operation);
                var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var types = allAssemblies.SelectMany(x => x.GetTypes());
                var inheritingTypes = types.Where(t => parent.IsAssignableFrom(t) && !t.IsAbstract).ToList();

                _operationsUses = inheritingTypes.Select(type => (Operation)Activator.CreateInstance(type)).ToList();
            }

            return _operationsUses.FirstOrDefault(op => op.Name.Equals(name));
        }
    }
}
