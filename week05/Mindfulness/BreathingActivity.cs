using System;
using System.Diagnostics;

public class BreathingActivity : Activity
{
    public BreathingActivity() 
        : base("Breathing Activity", 
               "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.Clear();
        
        int elapsedTime = 0;
        int breathCycleSeconds = 4; // 4 seconds in, 4 seconds out
        
        while (elapsedTime < GetDurationInSeconds())
        {
            // Breathe in
            Console.Write("Breathe in...");
            ShowCountDown(breathCycleSeconds);
            elapsedTime += breathCycleSeconds;
            
            if (elapsedTime >= GetDurationInSeconds())
                break;
            
            // Breathe out
            Console.Write("Breathe out...");
            ShowCountDown(breathCycleSeconds);
            elapsedTime += breathCycleSeconds;
        }
        
        DisplayEndingMessage();
    }
}