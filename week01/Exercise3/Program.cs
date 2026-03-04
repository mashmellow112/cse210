using System;

class Program
{
    static void Main(string[] args)
    {
        // Main game loop - allows playing multiple times
        string playAgain = "yes";

        while (playAgain.ToLower() == "yes")
        {
            // Generate a random magic number between 1 and 100
            Random random = new Random();
            int magicNumber = random.Next(1, 101);

            // Track number of guesses
            int guessCount = 0;

            Console.WriteLine("\n=== Guess My Number ===");
            Console.WriteLine("I'm thinking of a number between 1 and 100.");

            // Loop until user guesses the magic number
            while (true)
            {
                Console.Write("What is your guess? ");
                string guessInput = Console.ReadLine();
                int userGuess = int.Parse(guessInput);

                guessCount++;

                // Determine if the user needs to guess higher or lower, or if they guessed it
                if (userGuess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    Console.WriteLine($"It took you {guessCount} guess(es) to find the number.");
                    break; // Exit the loop when the user guesses correctly
                }
            }

            // Ask if user wants to play again
            Console.Write("\nDo you want to play again? (yes/no): ");
            playAgain = Console.ReadLine();
        }

        Console.WriteLine("Thanks for playing!");
    }
}
