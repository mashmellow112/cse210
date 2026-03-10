using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Manages a collection of journal entries.
/// </summary>
public class Journal
{
    // Private list of entries (abstraction)
    private List<Entry> _entries;

    // Constructor
    public Journal()
    {
        _entries = new List<Entry>();
    }

    /// <summary>
    /// Adds a new entry to the journal.
    /// </summary>
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    /// <summary>
    /// Displays all entries in the journal.
    /// </summary>
    public void DisplayAllEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No journal entries found.");
            return;
        }

        Console.WriteLine("\n===== Journal Entries =====");
        foreach (Entry entry in _entries)
        {
            Console.WriteLine(entry.ToDisplayString());
            Console.WriteLine("---------------------------");
        }
    }

    /// <summary>
    /// Saves the journal to a file.
    /// </summary>
    public void SaveToFile(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in _entries)
                {
                    writer.WriteLine(entry.ToFileString());
                }
            }
            Console.WriteLine($"Journal saved to {filename} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads the journal from a file.
    /// </summary>
    public void LoadFromFile(string filename)
    {
        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File {filename} not found.");
                return;
            }

            _entries.Clear(); // Clear existing entries
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Entry entry = Entry.FromFileString(line);
                    _entries.Add(entry);
                }
            }
            Console.WriteLine($"Journal loaded from {filename} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
        }
    }

    /// <summary>
    /// Gets the number of entries.
    /// </summary>
    public int GetEntryCount()
    {
        return _entries.Count;
    }

    /// <summary>
    /// Clears all entries from the journal.
    /// </summary>
    public void Clear()
    {
        _entries.Clear();
    }
}
