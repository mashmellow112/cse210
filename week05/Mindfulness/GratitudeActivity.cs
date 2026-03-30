using System;
using System.Collections.Generic;

public class GratitudeActivity : Activity
{
    private List<string> _gratitudeAreas = new List<string>
    {
        "List things you are grateful for today",
        "List people who have made a difference in your life",
        "List simple pleasures you often take for granted",
        "List challenges that helped you grow",
        "List moments of joy from this week"
    };

    private List<int> _usedAreaIndices = new List<int>();

    public GratitudeActivity() 
        : base("Gratitude Activity", 
               "This activity will help you cultivate a sense of gratitude and appreciation for the good things in your life.")
    {
    }

    private string GetRandomArea()
    {
        if (_usedAreaIndices.Count >= _gratitudeAreas.Count)
        {
            _usedAreaIndices.Clear();
        }

        Random random = new Random();
        int index;
        do
        {
            index = random.Next(_gratitudeAreas.Count);
        } while (_usedAreaIndices.Contains(index));

        _usedAreaIndices.Add(index);
        return _gratitudeAreas[index];
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.Clear();

        string area = GetRandomArea();
        Console.WriteLine("Focus on gratitude with the following prompt:");
        Console.WriteLine();
        Console.WriteLine(area);
        Console.WriteLine();
        Console.WriteLine("Take a deep breath and let gratitude fill your heart...");
        ShowSpinner(5);

        Console.Clear();
        Console.WriteLine("As you breathe, visualize each thing you are grateful for.");
        Console.WriteLine("Feel the warmth and peace that gratitude brings.");
        ShowSpinner(5);

        Console.Clear();
        Console.WriteLine("Now, take a moment to silently express thanks for:");
        Console.WriteLine("- People in your life");
        ShowSpinner(4);
        Console.WriteLine("- Your health and abilities");
        ShowSpinner(4);
        Console.WriteLine("- Small everyday blessings");
        ShowSpinner(4);
        Console.WriteLine("- Opportunities and experiences");
        ShowSpinner(4);
        Console.WriteLine("- The beauty around you");
        ShowSpinner(4);

        Console.Clear();
        Console.WriteLine("Take one more deep breath and carry this gratitude with you.");
        ShowSpinner(5);

        DisplayEndingMessage();
    }
}