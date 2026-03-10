using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        // EXCEEDING REQUIREMENTS:
        // 1. Added mood tracking to each entry - users can record their emotional state
        // 2. Added tags/labels for entries - users can categorize their journal entries
        // 3. Added entry count display - shows total entries in the journal
        // 4. Enhanced prompts list - 10 prompts instead of minimum 5
        // 5. Added time of day tracking (morning/afternoon/evening)
        // 6. Added Clear all entries option for fresh start
        // 7. Personalized greeting based on time of day

        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        bool running = true;
        while (running)
        {
            DisplayGreeting();
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal, promptGenerator);
                    break;
                case "2":
                    DisplayJournal(journal);
                    break;
                case "3":
                    SaveJournal(journal);
                    break;
                case "4":
                    LoadJournal(journal);
                    break;
                case "5":
                    ShowStatistics(journal);
                    break;
                case "6":
                    ClearJournal(journal);
                    break;
                case "7":
                    Console.WriteLine("Thank you for journaling! Have a great day!");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayGreeting()
    {
        DateTime now = DateTime.Now;
        string greeting;
        
        if (now.Hour < 12)
            greeting = "Good morning";
        else if (now.Hour < 17)
            greeting = "Good afternoon";
        else
            greeting = "Good evening";

        Console.WriteLine($"\n{greeting}! Welcome to your Personal Journal");
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\n===== Journal Menu =====");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save journal to file");
        Console.WriteLine("4. Load journal from file");
        Console.WriteLine("5. Show statistics");
        Console.WriteLine("6. Clear all entries");
        Console.WriteLine("7. Quit");
        Console.Write("Choose an option (1-7): ");
    }

    static void WriteNewEntry(Journal journal, PromptGenerator promptGenerator)
    {
        Console.WriteLine("\n--- Write a New Entry ---");
        
        // Get random prompt
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        
        // Get current date
        string date = DateTime.Now.ToString("yyyy-MM-dd");
        
        // Get time of day
        string timeOfDay = GetTimeOfDay();
        
        // Get mood (exceeding requirement)
        Console.Write("How are you feeling today? (happy/sad/excited/calm/stressed/tired): ");
        string mood = Console.ReadLine();
        
        // Get tags (exceeding requirement)
        Console.Write("Add tags (comma separated): ");
        string tags = Console.ReadLine();
        
        // Create entry with enhanced information
        Entry entry = new Entry($"{prompt}|{timeOfDay}|{mood}|{tags}", response, date);
        journal.AddEntry(entry);
        
        Console.WriteLine("Entry saved successfully!");
    }

    static string GetTimeOfDay()
    {
        int hour = DateTime.Now.Hour;
        if (hour < 12)
            return "Morning";
        else if (hour < 17)
            return "Afternoon";
        else
            return "Evening";
    }

    static void DisplayJournal(Journal journal)
    {
        Console.WriteLine("\n--- Your Journal ---");
        journal.DisplayAllEntries();
    }

    static void SaveJournal(Journal journal)
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Invalid filename.");
            return;
        }
        
        journal.SaveToFile(filename);
    }

    static void LoadJournal(Journal journal)
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Invalid filename.");
            return;
        }
        
        journal.LoadFromFile(filename);
    }

    static void ShowStatistics(Journal journal)
    {
        Console.WriteLine("\n--- Journal Statistics ---");
        Console.WriteLine($"Total entries: {journal.GetEntryCount()}");
        
        if (journal.GetEntryCount() > 0)
        {
            Console.WriteLine("Keep writing! Every entry is a memory preserved.");
        }
        else
        {
            Console.WriteLine("Start writing your first entry!");
        }
    }

    static void ClearJournal(Journal journal)
    {
        Console.Write("Are you sure you want to clear all entries? (yes/no): ");
        string confirm = Console.ReadLine().ToLower();
        
        if (confirm == "yes")
        {
            journal.Clear();
            Console.WriteLine("All entries cleared.");
        }
        else
        {
            Console.WriteLine("Clear cancelled.");
        }
    }
}
