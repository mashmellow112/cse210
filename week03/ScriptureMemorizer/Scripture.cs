using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a complete scripture with reference and text.
/// </summary>
public class Scripture
{
    private ScriptureReference _reference;
    private List<Word> _words;
    private static Random _random = new Random();

    /// <summary>
    /// Constructor that creates a scripture from a reference and text.
    /// </summary>
    /// <param name="reference">The scripture reference.</param>
    /// <param name="text">The full text of the scripture.</param>
    public Scripture(ScriptureReference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        
        // Split text into words and create Word objects
        string[] textWords = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in textWords)
        {
            // Remove punctuation from the word but keep it for display
            _words.Add(new Word(word));
        }
    }

    /// <summary>
    /// Constructor that creates a scripture from book, chapter, verse(s), and text.
    /// </summary>
    /// <param name="book">The book name.</param>
    /// <param name="chapter">The chapter number.</param>
    /// <param name="startVerse">The starting verse number.</param>
    /// <param name="endVerse">The ending verse number (optional, -1 for single verse).</param>
    /// <param name="text">The full text of the scripture.</param>
    public Scripture(string book, int chapter, int startVerse, int endVerse, string text)
    {
        if (endVerse == -1)
        {
            _reference = new ScriptureReference(book, chapter, startVerse);
        }
        else
        {
            _reference = new ScriptureReference(book, chapter, startVerse, endVerse);
        }
        
        _words = new List<Word>();
        
        // Split text into words and create Word objects
        string[] textWords = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in textWords)
        {
            _words.Add(new Word(word));
        }
    }

    /// <summary>
    /// Gets the scripture reference.
    /// </summary>
    public ScriptureReference GetReference()
    {
        return _reference;
    }

    /// <summary>
    /// Gets all words in the scripture.
    /// </summary>
    public List<Word> GetWords()
    {
        return _words;
    }

    /// <summary>
    /// Checks if all words in the scripture are hidden.
    /// </summary>
    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }

    /// <summary>
    /// Hides a specified number of random words that are not already hidden.
    /// </summary>
    /// <param name="count">The number of words to hide.</param>
    public void HideRandomWords(int count = 3)
    {
        // Get list of unhidden words
        var unhiddenWords = _words.Where(w => !w.IsHidden()).ToList();
        
        if (unhiddenWords.Count == 0)
        {
            return; // All words already hidden
        }

        // Determine how many words to hide (can't exceed unhidden count)
        int wordsToHide = Math.Min(count, unhiddenWords.Count);

        // Randomly select and hide words from unhidden words
        // Using shuffle approach for true randomness
        var shuffled = unhiddenWords.OrderBy(x => _random.Next()).Take(wordsToHide).ToList();
        
        foreach (var word in shuffled)
        {
            word.Hide();
        }
    }

    /// <summary>
    /// Returns the full display text of the scripture with reference.
    /// </summary>
    public string GetDisplayText()
    {
        string display = _reference.GetDisplayText() + "\n\n";
        
        foreach (Word word in _words)
        {
            display += word.GetDisplayText() + " ";
        }
        
        return display.Trim();
    }

    /// <summary>
    /// Gets the count of visible (not hidden) words.
    /// </summary>
    public int GetVisibleWordCount()
    {
        return _words.Count(w => !w.IsHidden());
    }
}
