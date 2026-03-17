using System;

class Program
{
    static void Main(string[] args)
    {
        // Test constructor with no parameters (1/1)
        Fraction fraction1 = new Fraction();
        Console.WriteLine(fraction1.GetFractionString());
        Console.WriteLine(fraction1.GetDecimalValue());

        // Test constructor with one parameter (5/1)
        Fraction fraction2 = new Fraction(5);
        Console.WriteLine(fraction2.GetFractionString());
        Console.WriteLine(fraction2.GetDecimalValue());

        // Test constructor with two parameters (6/7)
        Fraction fraction3 = new Fraction(6, 7);
        Console.WriteLine(fraction3.GetFractionString());
        Console.WriteLine(fraction3.GetDecimalValue());

        // Additional test cases as shown in sample output
        // Test 3/4
        Fraction fraction4 = new Fraction(3, 4);
        Console.WriteLine(fraction4.GetFractionString());
        Console.WriteLine(fraction4.GetDecimalValue());

        // Test 1/3
        Fraction fraction5 = new Fraction(1, 3);
        Console.WriteLine(fraction5.GetFractionString());
        Console.WriteLine(fraction5.GetDecimalValue());

        // Test getters and setters
        Fraction fraction6 = new Fraction(1, 1);
        Console.WriteLine($"Before setter - Top: {fraction6.GetTop()}, Bottom: {fraction6.GetBottom()}");
        fraction6.SetTop(5);
        fraction6.SetBottom(8);
        Console.WriteLine($"After setter - Top: {fraction6.GetTop()}, Bottom: {fraction6.GetBottom()}");
        Console.WriteLine(fraction6.GetFractionString());
        Console.WriteLine(fraction6.GetDecimalValue());
    }
}
