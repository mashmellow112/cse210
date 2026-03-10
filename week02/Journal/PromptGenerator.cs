using System;
using System.Collections.Generic;

/// <summary>
/// Generates random prompts for journal entries.
/// </summary>
public class PromptGenerator
{
    // Private list of prompts (abstraction)
    private List<string> _prompts;

    // Constructor - initialize with prompts
    public PromptGenerator()
    {
        _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is something new I learned today?",
            "What are three things I'm grateful for today?",
            "What challenged me today and how did I handle it?",
            "What is something I want to remember about today?",
            "Who made me smile today and why?"
        };
    }

    /// <summary>
    /// Gets a random prompt from the list.
    /// </summary>
    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }

    /// <summary>
    /// Gets all available prompts.
    /// </summary>
    public List<string> GetAllPrompts()
    {
        return _prompts;
    }

    /// <summary>
    /// Adds a new prompt to the list.
    /// </summary>
    public void AddPrompt(string prompt)
    {
        _prompts.Add(prompt);
    }
}
