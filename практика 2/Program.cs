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
        RpnCalc rpn = new RpnCalc(userText);
        Console.WriteLine(rpn.value);
    }
}