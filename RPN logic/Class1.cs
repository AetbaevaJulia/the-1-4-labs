using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RPN_logic
{
    internal class Token
    {

    }

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

    internal class Operation : Token
    {
        public char Symbol { get; set; }
        public int Priority { get; set; }

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

    public class RpnCalc
    {
        private List<Token> rpn;
        public double value;
        public RpnCalc(string mathExpression)
        {
            rpn = ToRPN(Parse(mathExpression));
            value = Calculate(rpn);
        }
        static List<Token> Parse(string userText)
        {
            userText = userText.Replace(" ", "");
            string buffer = string.Empty;
            List<Token> result = new List<Token>();
            for (int i = 0; i < userText.Length; i++)
            {
                if (Number.IsDigitNumber(userText[i]) || userText[i] == ',')
                {
                    buffer += userText[i];
                }
                else
                {
                    if (buffer != string.Empty)
                    {
                        double num = Convert.ToDouble(buffer);
                        result.Add(new Number(num));
                        buffer = string.Empty;
                    }
                    if (userText[i] != ')' && userText[i] != '(')
                    {
                        result.Add(new Operation(userText[i]));
                    }
                    else
                    {
                        result.Add(new Parenthesis(userText[i]));

                    }
                }
                if ((i == userText.Length - 1) && (buffer != string.Empty))
                {
                    double num = Convert.ToDouble(buffer);
                    result.Add(new Number(num));
                    buffer = string.Empty;
                }
            }
            return result;
        }

        static List<Token> ToRPN(List<Token> token)
        {
            List<Token> result = new List<Token>();
            Stack<Token> oper = new Stack<Token>();
            for (int i = 0; i < token.Count; i++)
            {
                if (token[i] is Number num)
                {
                    result.Add(num);
                }
                else if (token[i] is Operation operation)
                {
                    if (oper.Count == 0)
                    {
                        oper.Push(token[i]);
                        continue;
                    }
                    if (!(oper.Peek() is Parenthesis))
                    {
                        Operation operPeek = (Operation)oper.Peek();
                        if (operation.Priority > operPeek.Priority)
                        {
                            oper.Push(token[i]);
                        }
                        else if (operation.Priority <= operPeek.Priority)
                        {
                            while (oper.Count > 0 && !(token is Parenthesis))
                            {
                                result.Add(oper.Peek());
                                oper.Pop();
                            }
                            oper.Push(token[i]);
                        }
                    }
                    else
                    {
                        oper.Push(token[i]);
                        continue;
                    }
                }
                else if (token[i] is Parenthesis par)
                {
                    if (oper.Count == 0)
                    {
                        oper.Push(token[i]);
                        continue;
                    }
                    if (Parenthesis.IsClosedParenthesis(par.Symbol))
                    {
                        while (!(oper.Peek() is Parenthesis))
                        {
                            result.Add(oper.Peek());
                            oper.Pop();
                        }
                        oper.Pop();
                    }
                    else
                    {
                        oper.Push(token[i]);
                    }
                }
                if (i == token.Count - 1 && oper.Count != 0)
                {
                    while (oper.Count != 0)
                    {
                        result.Add(oper.Peek());
                        oper.Pop();
                    }
                }

            }
            return result;
        }

        static double PerformTheOperation(double num1, double num2, Operation oper)
        {
            if (oper.Symbol == '+') return num1 + num2;
            else if (oper.Symbol == '-') return num1 - num2;
            else if (oper.Symbol == '*') return num1 * num2;
            else return num1 / num2;
        }

        static double Calculate(List<Token> rpn)
        {
            Stack<double> res = new Stack<double>();
            for (int i = 0; i < rpn.Count; i++)
            {
                if (rpn[i] is Number number)
                {
                    res.Push(number.Value);
                }
                else if (rpn[i] is Operation oper)
                {
                    double secondNum = res.Pop(), firstNum = res.Pop();
                    res.Push(PerformTheOperation(firstNum, secondNum, oper));
                }
            }
            return res.Peek();
        }
    }
}