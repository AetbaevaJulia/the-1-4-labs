using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RPN_logic
{
    public class Token {}

    public class RpnCalc
    {
        private List<Token> _RPN;
        public double Value;
        public RpnCalc(string mathExpression, double valueX)
        {
            _RPN = ToRPN(ParseMathExpression(mathExpression));
            Value = CalculateWithX(_RPN, valueX);
        }

        static List<Token> ParseMathExpression(string userText)
        {
            string buffer = string.Empty;
            List<Token> result = new List<Token>();
            for (int i = 0; i < userText.Length; i++)
            {
                if (Number.IsDigitNumber(userText[i]) || userText[i] == ',')
                {
                    buffer += userText[i];
                }
                else if (char.IsLetter(userText[i]))
                {
                    if (buffer != string.Empty && char.IsDigit(buffer[0]))
                    {
                        result.Add(new Number(buffer));
                        buffer = string.Empty;
                    }
                    buffer += userText[i];
                }
                else
                {
                    if (buffer != string.Empty)
                    {
                        Operation oper = CreatorToken.CreateOperation(buffer);
                        if (oper != null && oper.IsFunc)
                        {
                            result.Add(oper);
                        }
                        else if (char.IsLetter(buffer[0]))
                        {
                            result.Add(new Variable(buffer));
                        }
                        else
                        {
                            result.Add(new Number(buffer));
                        }
                        buffer = string.Empty;
                    }

                    if (Parenthesis.IsParenthesis(userText[i]))
                    {
                        result.Add(CreatorToken.Create(userText[i]));
                    }
                    else
                    {
                        result.Add(CreatorToken.Create(Convert.ToString(userText[i])));
                    }
                }

                if ((i == userText.Length - 1) && (buffer != string.Empty))
                {
                    Operation oper = CreatorToken.CreateOperation(buffer);
                    if (oper != null && oper.IsFunc)
                    {
                        result.Add(oper);
                    }
                    else if (char.IsLetter(buffer[0]))
                    {
                        result.Add(new Variable(buffer));
                    }
                    else
                    {
                        result.Add(new Number(buffer));
                    }
                    buffer = string.Empty;
                }
            }
            return result;
        }
        static List<Token> ToRPN(List<Token> token)
        {
            List<Token> result = new List<Token>();
            Stack<Token> stack = new Stack<Token>();
            for (int i = 0; i < token.Count; i++)
            {
                if (token[i] is Number || token[i] is Variable var)
                {
                    result.Add(token[i]);
                }

                else if (token[i] is Operation operation)
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(operation);
                    }
                    else
                    {
                        if (stack.Peek() is not Parenthesis)
                        {
                            Operation peek = (Operation)stack.Peek();
                            if (operation.Priority > peek.Priority)
                            {
                                stack.Push(operation);
                            }
                            else
                            {
                                while (stack.Count > 0 && stack.Peek() is not Parenthesis)
                                {
                                    result.Add(stack.Pop());
                                }
                                stack.Push(operation);
                            }
                        }
                        else
                        {
                            stack.Push(token[i]);
                        }
                    }
                }

                else if (token[i] is Parenthesis par)
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(par);
                    }

                    if (Parenthesis.IsClosedParenthesis(par.Symbol))
                    { 
                        while (stack.Count > 0 && !(stack.Peek() is Parenthesis))
                        {
                            result.Add(stack.Pop());
                        }
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(par);
                    }
                }
                
                if (i == token.Count - 1 && stack.Count != 0)
                {
                    while (stack.Count != 0)
                    {
                        result.Add(stack.Peek());
                        stack.Pop();
                    }
                }
            }
            return result;
        }

        static double CalculateWithX(List<Token> rpn, double valueX)
        {
            Stack<Number> number = new Stack<Number>();
            for (int i = 0; i < rpn.Count; i++)
            {
                if (rpn[i] is Number num)
                {
                    number.Push(num);
                }
                else if (rpn[i] is Variable x)
                {
                    number.Push(new Number(valueX));
                }
                else
                {
                    Operation oper = (Operation)rpn[i];
                    Number[] args = new Number[oper.CountArgs];
                    for (int c = oper.CountArgs - 1; c >= 0; c--)
                    {
                        args[c] = number.Pop();
                    }
                    Number result = oper.Perform(args);
                    number.Push(result);
                }
            }
            return number.Peek().Value;
        }
    }
}