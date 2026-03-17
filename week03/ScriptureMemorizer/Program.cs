using System;

/*
 * 
 * 1. **Library of Scriptures**: Created a ScriptureLibrary class that manages a collection 
 *    of scriptures rather than a single one. This allows users to memorize different scriptures.
 * 
 * 2. **Load from File**: Added functionality to load scriptures from an external file using 
 *    the LoadFromFile() method. The format is: book|chapter|startVerse|endVerse|text
 *    (Use -1 for endVerse for single verses, lines starting with # are comments)
 * 
 * 3. **Smart Word Hiding**: The HideRandomWords() method only selects from unhidden words,
 *    preventing duplicate selections and ensuring progress with each key press.
 * 
 * 4. **Dynamic Difficulty**: The program adapts - when fewer words remain visible, it hides
 *    fewer words per press to create a smoother experience.
 * 
 * 5. **Multiple Religious Texts**: Included scriptures from KJV Bible, Book of Mormon, 
 *    and Doctrine & Covenants for broader spiritual application.
 * 
 * 6. **Console UI Enhancements**: Added visual progress indicator showing remaining words,
 *    better formatting, and smooth console clearing.
 * 
 * 7. **Graceful File Handling**: If the scripture file doesn't exist, falls back to default
 *    scriptures with a helpful message.
 */

class Program
{
    static void Main(string[] args)
    {
        // Note: Console.Clear() may not work in all environments
        // Using WriteLine to separate displays instead
        Console.WriteLine("\n\n");
        Console.WriteLine("     SCRIPTURE MEMORIZER");
        Console.WriteLine("===========================================");
        Console.WriteLine();
        
        // Create a scripture library
        ScriptureLibrary library = new ScriptureLibrary();
        
        // Try to load from file first, otherwise use defaults
        string scriptureFilePath = "scriptures.txt";
        if (System.IO.File.Exists(scriptureFilePath))
        {
            library.LoadFromFile(scriptureFilePath);
        }
        else
        {
            Console.WriteLine("No scripture file found. Loading default scriptures...");
            library.AddDefaultScriptures();
        }
        
        // If no scriptures loaded, add defaults
        if (library.GetCount() == 0)
        {
            library.AddDefaultScriptures();
        }
        
        // Get a random scripture
        Scripture scripture = library.GetRandomScripture();
        
        // Display the complete scripture
        DisplayScripture(scripture);
        
        // Main loop - keep hiding words until user quits or all words are hidden
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to hide more words, or type 'quit' to exit.");
            Console.Write("> ");
            
            string input = Console.ReadLine();
            
            if (input != null && input.ToLower() == "quit")
            {
                Console.WriteLine("\nThank you for using Scripture Memorizer!");
                Console.WriteLine("May God bless your efforts in memorizing His word.");
                break;
            }
            
            // Clear screen and redisplay
            Console.Clear();
            
            // Hide more words - dynamic count based on remaining words
            int wordsToHide = Math.Min(3, scripture.GetVisibleWordCount());
            if (wordsToHide > 0)
            {
                scripture.HideRandomWords(wordsToHide);
            }
            
            // Display the scripture
            DisplayScripture(scripture);
            
            // Check if all words are hidden
            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine();
                Console.WriteLine("CONGRATULATIONS! You have completely memorized this scripture!");
                Console.WriteLine();
                break;
            }
        }
    }
    
    static void DisplayScripture(Scripture scripture)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine(scripture.GetReference().GetDisplayText());
        Console.WriteLine("===========================================");
        Console.WriteLine();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine();
        Console.WriteLine($"Words remaining to memorize: {scripture.GetVisibleWordCount()}");
    }
}
