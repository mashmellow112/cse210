/// <summary>
/// Represents a scripture reference (e.g., "John 3:16" or "Proverbs 3:5-6").
/// </summary>
public class ScriptureReference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    /// <summary>
    /// Constructor for a single verse reference (e.g., "John 3:16").
    /// </summary>
    /// <param name="book">The book name.</param>
    /// <param name="chapter">The chapter number.</param>
    /// <param name="verse">The verse number.</param>
    public ScriptureReference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
    }

    /// <summary>
    /// Constructor for a verse range reference (e.g., "Proverbs 3:5-6").
    /// </summary>
    /// <param name="book">The book name.</param>
    /// <param name="chapter">The chapter number.</param>
    /// <param name="startVerse">The starting verse number.</param>
    /// <param name="endVerse">The ending verse number.</param>
    public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    /// <summary>
    /// Gets the book name.
    /// </summary>
    public string GetBook()
    {
        return _book;
    }

    /// <summary>
    /// Gets the chapter number.
    /// </summary>
    public int GetChapter()
    {
        return _chapter;
    }

    /// <summary>
    /// Gets the start verse number.
    /// </summary>
    public int GetStartVerse()
    {
        return _startVerse;
    }

    /// <summary>
    /// Gets the end verse number.
    /// </summary>
    public int GetEndVerse()
    {
        return _endVerse;
    }

    /// <summary>
    /// Returns the reference as a formatted string.
    /// </summary>
    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}
