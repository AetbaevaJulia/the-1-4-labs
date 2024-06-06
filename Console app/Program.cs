using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using RPN_logic;

class Program
{
    static void Main()
    {
        Console.Write("Введите математическое выражение: ");
        string userText = Console.ReadLine();
        Console.Write("Введите значение Х: ");
        int valueX = Convert.ToInt32(Console.ReadLine());
        RpnCalc rpn = new RpnCalc(userText, valueX);
        Console.WriteLine(rpn.Value);
    }
}