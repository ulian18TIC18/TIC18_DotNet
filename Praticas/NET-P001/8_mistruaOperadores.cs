using System;

public class MisturaOperadores
{
    static void Main()
    {
        int num1 = 7;
        int num2 = 3;
        int num3 = 10;

        bool comparacao = num1 > num2;
        Console.WriteLine($"O {num1} é maior que o {num2}: {comparacao}");

        bool comparacao2 = num1 + num2 == num3;
        Console.WriteLine($"A soma de {num1} com {num2} é igual a {num3}: {comparacao2}");
    }
}