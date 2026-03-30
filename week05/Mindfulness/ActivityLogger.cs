using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ActivityLogger
{
    private Dictionary<string, int> _activityCounts = new Dictionary<string, int>();
    private string _logFilePath;
    private List<string> _usedActivities = new List<string>();
    private Dictionary<string, List<string>> _usedPrompts = new Dictionary<string, List<string>>();

    public ActivityLogger()
    {
        string directory = Directory.GetCurrentDirectory();
        _logFilePath = Path.Combine(directory, "activity_log.txt");
        LoadLog();
    }

    public void RecordActivity(string activityName)
    {
        if (_activityCounts.ContainsKey(activityName))
        {
            _activityCounts[activityName]++;
        }
        else
        {
            _activityCounts[activityName] = 1;
        }

        if (!_usedActivities.Contains(activityName))
        {
            _usedActivities.Add(activityName);
        }

        SaveLog();
    }

    public int GetActivityCount(string activityName)
    {
        return _activityCounts.ContainsKey(activityName) ? _activityCounts[activityName] : 0;
    }

    public Dictionary<string, int> GetAllActivityCounts()
    {
        return new Dictionary<string, int>(_activityCounts);
    }

    public void DisplayLog()
    {
        Console.WriteLine("=== Activity Log ===");
        if (_activityCounts.Count == 0)
        {
            Console.WriteLine("No activities recorded yet.");
        }
        else
        {
            foreach (var entry in _activityCounts.OrderByDescending(e => e.Value))
            {
                Console.WriteLine($"{entry.Key}: {entry.Value} time(s)");
            }
        }
        Console.WriteLine();
    }

    public bool HasUsedAllActivities(List<string> allActivities)
    {
        foreach (var activity in allActivities)
        {
            if (!_usedActivities.Contains(activity))
            {
                return false;
            }
        }
        return true;
    }

    public void ResetUsedActivities()
    {
        _usedActivities.Clear();
    }

    public bool IsPromptUsed(string activityName, string prompt)
    {
        if (!_usedPrompts.ContainsKey(activityName))
        {
            return false;
        }
        return _usedPrompts[activityName].Contains(prompt);
    }

    public void MarkPromptUsed(string activityName, string prompt)
    {
        if (!_usedPrompts.ContainsKey(activityName))
        {
            _usedPrompts[activityName] = new List<string>();
        }
        _usedPrompts[activityName].Add(prompt);
    }

    public bool HasUsedAllPrompts(string activityName, int totalPrompts)
    {
        if (!_usedPrompts.ContainsKey(activityName))
        {
            return false;
        }
        return _usedPrompts[activityName].Count >= totalPrompts;
    }

    public void ResetUsedPrompts(string activityName)
    {
        if (_usedPrompts.ContainsKey(activityName))
        {
            _usedPrompts[activityName].Clear();
        }
    }

    private void SaveLog()
    {
        try
        {
            List<string> lines = new List<string>();
            foreach (var entry in _activityCounts)
            {
                lines.Add($"{entry.Key}={entry.Value}");
            }
            File.WriteAllLines(_logFilePath, lines);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving log: {e.Message}");
        }
    }

    private void LoadLog()
    {
        try
        {
            if (File.Exists(_logFilePath))
            {
                string[] lines = File.ReadAllLines(_logFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        string activityName = parts[0];
                        if (int.TryParse(parts[1], out int count))
                        {
                            _activityCounts[activityName] = count;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading log: {e.Message}");
        }
    }
}