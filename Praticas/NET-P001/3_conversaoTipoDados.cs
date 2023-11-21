
using System;

class Conversao
{
    static void Main()
    {
        double numDouble = 10.75;

        // Conversão de double para int 
        int numInt = (int)numDouble;

        Console.WriteLine($"Número double: {numDouble}");
        //Resolvendo o problema com um trucamento.
        Console.WriteLine($"Número inteiro truncado: {numInt}");
    }
}
