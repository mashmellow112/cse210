using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Loop to get numbers from user
        while (true)
        {
            Console.Write("Enter number: ");
            string input = Console.ReadLine();
            int number = int.Parse(input);

            // Stop when user enters 0
            if (number == 0)
            {
                break;
            }

            // Add number to the list (but not 0)
            numbers.Add(number);
        }

        // Compute the sum
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        // Compute the average
        double average = (double)sum / numbers.Count;

        // Find the maximum
        int max = numbers[0];
        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }

        // Find the smallest positive number (closest to zero)
        int? smallestPositive = null;
        foreach (int number in numbers)
        {
            if (number > 0)
            {
                if (smallestPositive == null || number < smallestPositive)
                {
                    smallestPositive = number;
                }
            }
        }

        // Sort the numbers
        numbers.Sort();

        // Display results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");
        
        if (smallestPositive.HasValue)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }
        
        Console.WriteLine("The sorted list is:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}
