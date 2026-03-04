using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask user for their grade percentage
        Console.Write("What is your grade percentage? ");
        string input = Console.ReadLine();
        
        // Validate input is a valid integer
        if (!int.TryParse(input, out int grade))
        {
            Console.WriteLine("Invalid input. Please enter a numeric grade percentage.");
            return;
        }

        // Validate grade is within valid range (0-100)
        if (grade < 0 || grade > 100)
        {
            Console.WriteLine("Invalid grade. Please enter a percentage between 0 and 100.");
            return;
        }

        // Determine letter grade using if, statements
        string letter;
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine the sign (+ or -)
        string sign = "";
        int lastDigit = grade % 10;

        if (grade >= 60) // Only add sign for passing grades (C or above)
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        // Handle special cases: no A+
        if (letter == "A" && sign == "+")
        {
            sign = "";
        }

        // Handle special cases: no F+ or F-
        if (letter == "F")
        {
            sign = "";
        }

        // Display the letter grade with sign
        Console.WriteLine($"Your letter grade is: {letter}{sign}");

        // Determine if user passed (>= 70)
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course!");
        }
        else
        {
            Console.WriteLine("Don't give up! Keep working hard for next time.");
        }
    }
}
