using System;
using System.Diagnostics;

public abstract class Activity
{
    private const int MinimumDurationSeconds = 1;
    private const int MaximumDurationSeconds = 3600; // 1 hour max

    private string _name;
    private string _description;
    private int _durationInSeconds;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public string GetName() => _name;
    public string GetDescription() => _description;
    public int GetDurationInSeconds() => _durationInSeconds;

    protected void SetDurationInSeconds(int duration)
    {
        _durationInSeconds = duration;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        
        int duration = GetValidDurationFromUser();
        SetDurationInSeconds(duration);
        
        Console.WriteLine();
        Console.Write("Get ready to begin...");
        ShowSpinner(5);
    }

    /// <summary>
    /// Prompts the user for a valid duration value with comprehensive error handling.
    /// </summary>
    /// <returns>A valid duration in seconds within the allowed range.</returns>
    private int GetValidDurationFromUser()
    {
        string input = Console.ReadLine();
        
        // First, check if input is null or empty
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.Write("Please enter a valid number: ");
            return GetValidDurationFromUser(); // Recursive retry
        }
        
        // Use TryParse to avoid exception overhead and handle invalid input gracefully
        if (int.TryParse(input.Trim(), out int duration))
        {
            // Validate the parsed value is within acceptable range
            if (duration < MinimumDurationSeconds)
            {
                Console.Write($"Duration must be at least {MinimumDurationSeconds} second. Please enter again: ");
                return GetValidDurationFromUser();
            }
            
            if (duration > MaximumDurationSeconds)
            {
                Console.Write($"Duration cannot exceed {MaximumDurationSeconds} seconds. Please enter again: ");
                return GetValidDurationFromUser();
            }
            
            return duration;
        }
        
        // Input was not a valid integer
        Console.Write("Invalid input. Please enter a whole number: ");
        return GetValidDurationFromUser();
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_durationInSeconds} seconds of the {_name}.");
        ShowSpinner(5);
    }

    protected void ShowSpinner(int seconds)
    {
        string[] spinnerFrames = { "|", "/", "-", "\\" };
        Stopwatch stopwatch = Stopwatch.StartNew();
        int frameIndex = 0;
        
        while (stopwatch.ElapsedMilliseconds < seconds * 1000)
        {
            Console.Write($"\r{spinnerFrames[frameIndex]}");
            frameIndex = (frameIndex + 1) % spinnerFrames.Length;
            Thread.Sleep(250);
        }
        Console.Write("\r ");
        Console.WriteLine();
    }

    protected void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"\r{i}");
            Thread.Sleep(1000);
        }
        Console.Write("\r   ");
        Console.WriteLine();
    }

    protected void ShowAnimatedPause(int seconds, string message)
    {
        Console.WriteLine(message);
        ShowSpinner(seconds);
    }

    public abstract void Run();
}
