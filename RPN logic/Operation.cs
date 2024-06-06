using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN_logic
{
    abstract class Operation : Token
    {
        public abstract string Name { get; }
        public abstract int Priority { get; }
        public abstract bool IsFunc { get; }
        public abstract int CountArgs { get; }
        public abstract Number Perform(params Number[] numbers);
    }

    class Pluse : Operation
    {
        public override string Name => "+";
        public override int Priority => 1;
        public override bool IsFunc => false;
        public override int CountArgs => 2;
        public override Number Perform(params Number[] numbers)
        {
            double num1 = numbers[0].Value, num2 = numbers[1].Value;
            return new Number(num1 + num2);
        }
    }

    class Minus : Operation
    {
        public override string Name => "-";
        public override int Priority => 1;
        public override bool IsFunc => false;
        public override int CountArgs => 2;
        public override Number Perform(params Number[] numbers)
        {
            double num1 = numbers[0].Value, num2 = numbers[1].Value;
            return new Number(num1 - num2);
        }
    }

    class Multiply : Operation
    {
        public override string Name => "*";
        public override int Priority => 2;
        public override bool IsFunc => false;
        public override int CountArgs => 2;
        public override Number Perform(params Number[] numbers)
        {
            double num1 = numbers[0].Value, num2 = numbers[1].Value;
            return new Number(num1 * num2);
        }
    }

    class Division : Operation
    {
        public override string Name => "/";
        public override int Priority => 2;
        public override bool IsFunc => false;
        public override int CountArgs => 2;
        public override Number Perform(params Number[] numbers)
        {
            double num1 = numbers[0].Value, num2 = numbers[1].Value;
            return new Number(num1 / num2);
        }
    }

    class Log : Operation
    {
        public override string Name => "log";
        public override int Priority => 3;
        public override bool IsFunc => true;
        public override int CountArgs => 2;
        public override Number Perform(params Number[] numbers)
        {
            double num1 = numbers[0].Value, num2 = numbers[1].Value;
            return new Number(Math.Log(num2, num1));
        }
    }

    class Degree : Operation
    {
        public override string Name => "^";
        public override int Priority => 3;
        public override bool IsFunc => true;
        public override int CountArgs => 2;
        public override Number Perform(params Number[] numbers)
        {
            double num1 = numbers[0].Value;
            double num2 = numbers[1].Value;
            return new Number(Math.Pow(num1, num2));
        }
    }
    class Sqrt : Operation
    {
        public override string Name => "sqrt";
        public override int Priority => 3;
        public override bool IsFunc => true;
        public override int CountArgs => 1;
        public override Number Perform(params Number[] numbers)
        {
            double num = numbers[0].Value;
            return new Number(Math.Sqrt(num));
        }
    }

    class Root : Operation
    {
        public override string Name => "rt";
        public override int Priority => 3;
        public override bool IsFunc => true;
        public override int CountArgs => 2;
        public override Number Perform(params Number[] numbers)
        {
            double num1 = 1 / numbers[0].Value;
            double num2 = numbers[1].Value;
            return new Number(Math.Pow(num2, num1));
        }
    }

    class Sin : Operation
    {
        public override string Name => "sin";
        public override int Priority => 3;
        public override bool IsFunc => true;
        public override int CountArgs => 1;
        public override Number Perform(params Number[] numbers)
        {
            double num = numbers[0].Value;
            return new Number(Math.Sin(num));
        }
    }

    class Cos : Operation
    {
        public override string Name => "cos";
        public override int Priority => 3;
        public override bool IsFunc => true;
        public override int CountArgs => 1;
        public override Number Perform(params Number[] numbers)
        {
            double num = numbers[0].Value;
            return new Number(Math.Cos(num));
        }
    }

    class Tg : Operation
    {
        public override string Name => "tg";
        public override int Priority => 3;
        public override bool IsFunc => true;
        public override int CountArgs => 1;
        public override Number Perform(params Number[] numbers)
        {
            double num = numbers[0].Value;
            return new Number(Math.Tan(num));
        }
    }

    class Ctg : Operation
    {
        public override string Name => "ctg";
        public override int Priority => 3;
        public override bool IsFunc => true;
        public override int CountArgs => 1;
        public override Number Perform(params Number[] numbers)
        {
            double num = numbers[0].Value;
            return new Number(1 / Math.Tan(num));
        }
    }
}
