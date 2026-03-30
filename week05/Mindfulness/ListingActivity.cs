using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private List<int> _usedPromptIndices = new List<int>();

    public ListingActivity() 
        : base("Listing Activity", 
               "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    private string GetRandomPrompt()
    {
        if (_usedPromptIndices.Count >= _prompts.Count)
        {
            _usedPromptIndices.Clear();
        }

        Random random = new Random();
        int index;
        do
        {
            index = random.Next(_prompts.Count);
        } while (_usedPromptIndices.Contains(index));

        _usedPromptIndices.Add(index);
        return _prompts[index];
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.Clear();

        string prompt = GetRandomPrompt();
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine(prompt);
        Console.WriteLine();
        Console.WriteLine("You have 5 seconds to begin...");
        ShowSpinner(5);

        Console.WriteLine();
        Console.WriteLine("Start listing your items! Press Enter after each item.");
        Console.WriteLine();

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(GetDurationInSeconds());

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                items.Add(input);
            }
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {items.Count} items!");
        
        DisplayEndingMessage();
    }
}