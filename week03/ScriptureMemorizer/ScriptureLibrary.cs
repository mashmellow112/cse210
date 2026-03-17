using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Manages a library of scriptures with functionality to load from file and select randomly.
/// </summary>
public class ScriptureLibrary
{
    private List<Scripture> _scriptures;
    private static Random _random = new Random();

    /// <summary>
    /// Creates an empty scripture library.
    /// </summary>
    public ScriptureLibrary()
    {
        _scriptures = new List<Scripture>();
    }

    /// <summary>
    /// Adds a scripture to the library.
    /// </summary>
    public void AddScripture(Scripture scripture)
    {
        _scriptures.Add(scripture);
    }

    /// <summary>
    /// Loads scriptures from a file.
    /// Expected format: book|chapter|startVerse|endVerse|text
    /// For single verses, endVerse should be -1
    /// </summary>
    /// <param name="filePath">Path to the scripture file.</param>
    public void LoadFromFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    continue; // Skip empty lines and comments
                }

                string[] parts = line.Split('|');
                if (parts.Length >= 5)
                {
                    string book = parts[0].Trim();
                    int chapter = int.Parse(parts[1].Trim());
                    int startVerse = int.Parse(parts[2].Trim());
                    int endVerse = int.Parse(parts[3].Trim());
                    string text = parts[4].Trim();

                    // Handle multiple pipe characters for text with pipes
                    if (parts.Length > 5)
                    {
                        for (int i = 5; i < parts.Length; i++)
                        {
                            text += "|" + parts[i].Trim();
                        }
                    }

                    _scriptures.Add(new Scripture(book, chapter, startVerse, endVerse, text));
                }
            }
            Console.WriteLine($"Loaded {_scriptures.Count} scriptures from file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scriptures from file: {ex.Message}");
        }
    }

    /// <summary>
    /// Gets a random scripture from the library.
    /// </summary>
    public Scripture GetRandomScripture()
    {
        if (_scriptures.Count == 0)
        {
            return null;
        }
        
        return _scriptures[_random.Next(_scriptures.Count)];
    }

    /// <summary>
    /// Gets all scriptures in the library.
    /// </summary>
    public List<Scripture> GetAllScriptures()
    {
        return _scriptures;
    }

    /// <summary>
    /// Gets the count of scriptures in the library.
    /// </summary>
    public int GetCount()
    {
        return _scriptures.Count;
    }

    /// <summary>
    /// Adds default scriptures to the library for offline functionality.
    /// </summary>
    public void AddDefaultScriptures()
    {
        // John 3:16
        _scriptures.Add(new Scripture("John", 3, 16, -1, 
            "For God so loved the world that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."));

        // Proverbs 3:5-6
        _scriptures.Add(new Scripture("Proverbs", 3, 5, 6, 
            "Trust in the LORD with all thine heart and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."));

        // Psalm 23:1
        _scriptures.Add(new Scripture("Psalm", 23, 1, -1, 
            "The LORD is my shepherd; I shall not want."));

        // Romans 8:28
        _scriptures.Add(new Scripture("Romans", 8, 28, -1, 
            "And we know that all things work together for good to them that love God, to them who are the called according to his purpose."));

        // Philippians 4:13
        _scriptures.Add(new Scripture("Philippians", 4, 13, -1, 
            "I can do all things through Christ which strengtheneth me."));

        // Isaiah 40:31
        _scriptures.Add(new Scripture("Isaiah", 40, 31, -1, 
            "But they that wait upon the LORD shall renew their strength; they shall mount up with wings as eagles; they shall run, and not be weary; and they shall walk, and not faint."));

        // 2 Nephi 2:25
        _scriptures.Add(new Scripture("2 Nephi", 2, 25, -1, 
            "Adam fell that men might be; and men are, that they might have joy."));

        // Mosiah 2:17
        _scriptures.Add(new Scripture("Mosiah", 2, 17, -1, 
            "When ye are in the service of your fellow beings ye are only in the service of your God."));

        // Moroni 10:4-5
        _scriptures.Add(new Scripture("Moroni", 10, 4, 5, 
            "And when ye shall receive these things, I would exhort you that ye would ask God, the Eternal Father, in the name of Christ, if these things are not true; and if ye shall ask with a sincere heart, with real intent, having faith in Christ, he will manifest the truth of it unto you, by the power of the Holy Ghost."));

        // D&C 4:5-6
        _scriptures.Add(new Scripture("D&C", 4, 5, 6, 
            "Trust in the Spirit that knoweth all things. Therefore, lift up your heart and rejoice, and cleave unto the covenants which ye have made."));

        Console.WriteLine($"Added {_scriptures.Count} default scriptures.");
    }
}
