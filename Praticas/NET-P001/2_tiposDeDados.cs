// See https://aka.ms/new-console-template for more information
using System;

public class Programa
{
    static void Main()
    {
        /* Os tipos de dados inteiros em .NET são: */

        sbyte num1 = 10;
        byte num2 = 255;
        short num3 = 2000;
        ushort num4 = 50000;
        int num5 = 1000000;
        uint num6 = 3000000000;
        long num7 = 9000000000000000000;
        ulong num8 = 15000000000000000000;

        Console.WriteLine("sbyte: " + num1);
        Console.WriteLine("byte: " + num2);
        Console.WriteLine("short: " + num3);
        Console.WriteLine("ushort: " + num4);
        Console.WriteLine("int: " + num5);
        Console.WriteLine("uint: " + num6);
        Console.WriteLine("long: " + num7);
        Console.WriteLine("ulong: " + num8);
    }
}
