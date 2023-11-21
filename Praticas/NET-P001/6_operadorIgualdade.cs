using System;

public class TesteIgualdade
{
    static void Main()
    {
        string str1 = "Hello";
        string str2 = "World";

        if (str1 == str2)
        {
            Console.WriteLine($"As stings {str1} e {str2} são iguais.");
        }
        else
        {
            Console.WriteLine($"As As stings {str1} e {str2} são diferentes.");
        }
    }
}