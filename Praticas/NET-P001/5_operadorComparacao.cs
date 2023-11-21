using System;

public class OperadorComparacao
{
    static void Main()
    {
        int a = 5;
        int b = 8;

        if (a > b)
        {
            Console.WriteLine($"O valor de a '{a}' é maior que b '{b}'.");
        }

        else if (a < b)
        {
            Console.WriteLine($"O valor de a '{a}' é menor que b '{b}'.");
        }

        else
        {
            Console.WriteLine($"Os números são iguais a '{a}'.");
        }
    }
}