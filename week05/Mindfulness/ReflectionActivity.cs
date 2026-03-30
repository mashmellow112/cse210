using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    private List<int> _usedPromptIndices = new List<int>();
    private List<int> _usedQuestionIndices = new List<int>();

    public ReflectionActivity() 
        : base("Reflection Activity", 
               "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
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

    private string GetRandomQuestion()
    {
        if (_usedQuestionIndices.Count >= _questions.Count)
        {
            _usedQuestionIndices.Clear();
        }

        Random random = new Random();
        int index;
        do
        {
            index = random.Next(_questions.Count);
        } while (_usedQuestionIndices.Contains(index));

        _usedQuestionIndices.Add(index);
        return _questions[index];
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.Clear();

        string prompt = GetRandomPrompt();
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine();
        Console.WriteLine(prompt);
        Console.WriteLine();
        Console.WriteLine("When you are ready, press Enter to continue...");
        Console.ReadLine();

        Console.WriteLine("Now reflect on this as you think about the following questions:");
        ShowSpinner(5);
        Console.Clear();

        int elapsedTime = 0;
        int questionPauseSeconds = 5;

        while (elapsedTime < GetDurationInSeconds())
        {
            string question = GetRandomQuestion();
            Console.WriteLine(question);
            ShowSpinner(questionPauseSeconds);
            elapsedTime += questionPauseSeconds;

            if (elapsedTime >= GetDurationInSeconds())
                break;

            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}