using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;

class Program
{
    static int Priority(char operators)
    {
        if (operators == '(' || operators == ')') return 0;
        else if (operators == '+' || operators == '-') return 1;
        else return 2;
    }

    static List<object> RPN(string userText) //метод для записи в ОПЗ
    {
        userText = userText.Replace(" ", "");
        List<object> result = new List<object>();
        Stack<char> oper = new Stack<char> ();
        string bufer = string.Empty;
        for (int i = 0; i<userText.Length; i++)
        {
            if (char.IsDigit(userText[i]))
            {
                bufer += userText[i];
            }

            if (!char.IsDigit(userText[i]))
            {
                if (bufer != string.Empty)
                {
                    result.Add(bufer);
                    bufer = string.Empty;
                }
                if (oper.Count == 0)
                    oper.Push(userText[i]);
                else
                {
                    if (userText[i] == '(')
                        oper.Push(userText[i]);
                    else
                    {
                        if ((Priority(userText[i]) > Priority(oper.Peek())) && userText[i] != ')')
                            oper.Push(userText[i]);
                        else if (Priority(userText[i]) <= Priority(oper.Peek())  && userText[i] != ')')
                        {
                            if (oper.Contains('('))
                            {
                                while (oper.Peek() != '(')
                                {
                                    result.Add(oper.Peek());
                                    oper.Pop();
                                }
                                oper.Push(userText[i]);
                            }
                            else if ((Priority(userText[i]) < Priority(oper.Peek()) && (!oper.Contains('('))))
                            {
                                while (oper.Count != 0)
                                {
                                    result.Add(oper.Peek());
                                    oper.Pop();
                                }
                                oper.Push(userText[i]);
                            }

                            else if (Priority(userText[i]) == Priority(oper.Peek()) && (!oper.Contains('(')))
                            {
                                result.Add(oper.Peek());
                                oper.Pop();
                                oper.Push(userText[i]);
                            }
                        }
                        else if (userText[i] == ')')
                        {
                            while (oper.Peek() != '(')
                            {
                                result.Add(oper.Peek());
                                oper.Pop();
                            }
                            oper.Pop();
                        }
                    }
                }
            }

            if ((i == userText.Length - 1) && (bufer != string.Empty))
                result.Add((bufer));

            if ((i == userText.Length-1) && (oper.Count != 0))
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

    static bool IsOperator(char symbol) //метод для определения оператора
    {
        string operators = "+-*/";
        if (operators.Contains(symbol)) return true;
        else return false;
    }

    static double TheOperation (char oper, int num1, int num2)
    {
        if (oper == '+') return num1 + num2;
        else if (oper == '-') return num1 - num2;
        else if (oper == '*') return num1 * num2;
        else return num1 / num2;     
    }


    static List<object> Calculate(List<object> userText)
    { int i = 0;
        while (userText.Count > 1)
        {
            if (i > userText.Count)
                i = 0;
            if (userText[i] is char && IsOperator((char)userText[i]))
            {
                userText[i-2] = TheOperation((char)userText[i], Convert.ToInt16(userText[i - 2]), Convert.ToInt16(userText[i - 1]));
                userText.RemoveAt(i);
                userText.RemoveAt(i - 1);
                i = 0;
            }
            i++;
        }
        return userText;
    }
    static List<int> GetNums (List<object> userText) //метод для вывода чисел из начального выражения
    {
        List<int> res = new List<int>();
        foreach (var el in userText)
        {
            if (!(el is char))
                res.Add(Convert.ToInt32(el));
        }
        return res;
    }

    static List<char> GetOperators (List<object> userText) //метод для вывода операторов из начального выражения
    {
        List<char> res = new List<char>();
        foreach (var el in userText)
        {
            if (el is char)
                res.Add(Convert.ToChar(el));
        }
        return res;
    }

    static void Main ()
    {
        Console.Write("Your mathematical expression: ");
        string userText = Console.ReadLine();
        Console.WriteLine();
        List<object> rpnUserText = RPN(userText);
        Console.WriteLine("RPN: " + string.Join(" ", rpnUserText)+"\n");
        Console.WriteLine("The nums: " + string.Join(" ", GetNums(rpnUserText)) + "\n");
        Console.WriteLine("The operators: " + string.Join (" ", GetOperators(rpnUserText)) + "\n");
        Console.WriteLine("The answer: " + Calculate(rpnUserText)[0]);
    }
}