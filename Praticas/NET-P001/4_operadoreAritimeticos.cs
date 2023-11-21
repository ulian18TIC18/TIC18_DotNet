using System;

public class Operacoes
{
    static void Main()
    {
        int x = 10;
        int y = 3;

        Console.WriteLine(x + y);
        Console.WriteLine(x - y);
        Console.WriteLine(x * y);

        if (y != 0)
        {
            Console.WriteLine(x / y);
        }
        else
        {
            Console.WriteLine("A divisão por zero não pode ser realizada");
        }

    }
}