/*
 * Mindfulness Application - CSE 210 Week 05
 * 
 * EXCEEDING REQUIREMENTS IMPLEMENTATION:
 * 
 * 1. Fourth Activity Type: Added GratitudeActivity with visualization and breathing exercises
 * 
 * 2. Activity Logging System: 
 *    - Tracks how many times each activity is performed
 *    - Saves activity counts to activity_log.txt file
 *    - Displays statistics on request
 *    - Persists data between program runs
 * 
 * 3. No Repeated Prompts/Questions:
 *    - ReflectionActivity: Ensures prompts and questions don't repeat until all have been shown
 *    - ListingActivity: Ensures prompts don't repeat until all have been shown
 *    - GratitudeActivity: Ensures gratitude areas don't repeat until all have been shown
 *    - Uses tracking lists to remember used items and resets when exhausted
 * 
 * 4. File Persistence:
 *    - Activity counts are saved to activity_log.txt
 *    - Log is loaded when program starts
 *    - Data persists between sessions
 * 
 * 5. Breathing Animation:
 *    - BreathingActivity displays countdown timers with visual feedback
 *    - Spinner animations provide engaging interface during pauses
 *    - Clear visual indicators for inhale/exhale phases
 * 
 * 6. Additional Enhancements:
 *    - Color-coded console output for different activities
 *    - Duration-based activity timing
 *    - Menu-driven interface with activity statistics
 *    - Graceful error handling for file operations
 * 
 * Architecture:
 * - Activity (base class): Encapsulates common behavior for all activities
 * - BreathingActivity, ReflectionActivity, ListingActivity, GratitudeActivity: Inherit from Activity
 * - ActivityLogger: Manages activity tracking and file persistence
 * - Program: Main entry point with menu system
 */

using System;
using System.Collections.Generic;

class Program
{
    static ActivityLogger logger = new ActivityLogger();

    static void Main(string[] args)
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("     Welcome to the Mindfulness App          ");
        Console.WriteLine("==============================================");
        Console.WriteLine();

        bool running = true;
        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunActivity(new BreathingActivity(), "Breathing Activity");
                    break;
                case "2":
                    RunActivity(new ReflectionActivity(), "Reflection Activity");
                    break;
                case "3":
                    RunActivity(new ListingActivity(), "Listing Activity");
                    break;
                case "4":
                    RunActivity(new GratitudeActivity(), "Gratitude Activity");
                    break;
                case "5":
                    logger.DisplayLog();
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Thank you for using the Mindfulness App. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (running)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("Please select one of the following choices:");
        Console.WriteLine();
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");
        Console.WriteLine("4. Gratitude Activity");
        Console.WriteLine("5. View Activity Statistics");
        Console.WriteLine("6. Quit");
        Console.WriteLine();
        Console.Write("What would you like to do? ");
    }

    static void RunActivity(Activity activity, string activityName)
    {
        Console.Clear();
        activity.Run();
        logger.RecordActivity(activityName);
        Console.WriteLine();
        Console.WriteLine($"Activity completed! It has been logged.");
    }
}