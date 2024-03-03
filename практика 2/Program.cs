using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using практика_2;

class Program
{
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
            if (i == token.Count-1 && oper.Count != 0)
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

    static string ToString(List<Token> rpn)
    {
        string res = string.Empty;
        foreach (Token token in rpn)
        {
            if (token is Parenthesis par)
            {
                res += par.Symbol + " ";
            }
            else if (token is Operation oper)
            {
                res += oper.Symbol + " ";
            }
            else if (token is Number num)
            {
                res += num.Value + " ";
            }
        }
        return res;
    }

    static double PerformTheOperation(double num1, double num2, Operation oper)
    {
        if (oper.Symbol == '+') return num1 + num2;
        else if (oper.Symbol == '-') return num1 - num2;
        else if(oper.Symbol == '*') return num1 * num2;
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
                double firstNum = res.Pop(), secondNum = res.Pop();
                res.Push(PerformTheOperation(firstNum, secondNum, oper));
            }
        }
        return res.Peek();
    }
    
    static List<Token> GetNums(List<Token> rpn)
    {
        List<Token> result = new List<Token>();
        foreach (Token token in rpn)
        {
            if (token is Number)
            {
                result.Add((Number)token);
            }
        }
        return result;
    }

    static List<Token> GetOperators(List<Token> rpn)
    {
        List<Token> result = new List<Token> ();
        foreach (Token token in rpn)
        {
            if (token is Operation)
            {
                result.Add((Operation)token);
            }
        }
        return result;
    }

    static void Main()
    {
        Console.Write("Введите математическое выражение: ");
        string userText = Console.ReadLine();
        List<Token> tokens = Parse(userText);
        List<Token> rpn = ToRPN(tokens);
        List<Token> Nums = GetNums(rpn);
        List<Token> Operators = GetOperators(rpn);
        Console.WriteLine("Ваше выражение в ОПЗ: " + ToString(rpn));
        Console.WriteLine("Числа в вашем выражении: " + ToString(Nums));
        Console.WriteLine("Операвторы в вашем выражении: " + ToString(Operators));
        Console.WriteLine("Значение выражения: " + Calculate(rpn));
    }
}