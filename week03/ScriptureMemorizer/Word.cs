/// <summary>
/// Represents a single word in a scripture, with functionality to hide/reveal it.
/// </summary>
public class Word
{
    private string _text;
    private bool _isHidden;

    /// <summary>
    /// Constructor for creating a word with specified text.
    /// </summary>
    /// <param name="text">The text of the word.</param>
    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    /// <summary>
    /// Gets the original text of the word.
    /// </summary>
    public string GetText()
    {
        return _text;
    }

    /// <summary>
    /// Checks if the word is currently hidden.
    /// </summary>
    public bool IsHidden()
    {
        return _isHidden;
    }

    /// <summary>
    /// Hides the word by replacing it with underscores matching the word length.
    /// </summary>
    public void Hide()
    {
        _isHidden = true;
    }

    /// <summary>
    /// Reveals the word (resets to original text).
    /// </summary>
    public void Reveal()
    {
        _isHidden = false;
    }

    /// <summary>
    /// Returns the display text - either the original word or underscores if hidden.
    /// </summary>
    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return new string('_', _text.Length);
        }
        return _text;
    }

    /// <summary>
    /// Gets the length of the word (number of letters).
    /// </summary>
    public int GetLength()
    {
        return _text.Length;
    }
}
