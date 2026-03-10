using System;

/// <summary>
/// Represents a single journal entry with a prompt, response, and date.
/// </summary>
public class Entry
{
    // Member variables (abstraction)
    private string _prompt;
    private string _response;
    private string _date;

    // Constructor
    public Entry(string prompt, string response, string date)
    {
        _prompt = prompt;
        _response = response;
        _date = date;
    }

    // Getters and setters
    public string GetPrompt()
    {
        return _prompt;
    }

    public void SetPrompt(string prompt)
    {
        _prompt = prompt;
    }

    public string GetResponse()
    {
        return _response;
    }

    public void SetResponse(string response)
    {
        _response = response;
    }

    public string GetDate()
    {
        return _date;
    }

    public void SetDate(string date)
    {
        _date = date;
    }

    /// <summary>
    /// Converts the entry to a formatted string for display.
    /// </summary>
    public string ToDisplayString()
    {
        return $"Date: {_date}\nPrompt: {_prompt}\nResponse: {_response}\n";
    }

    /// <summary>
    /// Converts the entry to a string for file storage (using | as separator).
    /// </summary>
    public string ToFileString()
    {
        // Use | as separator since it's unlikely to appear in content
        return $"{_date}|{_prompt}|{_response}";
    }

    /// <summary>
    /// Creates an Entry from a file string.
    /// </summary>
    public static Entry FromFileString(string fileString)
    {
        string[] parts = fileString.Split('|');
        if (parts.Length >= 3)
        {
            return new Entry(parts[1], parts[2], parts[0]);
        }
        return new Entry("", "", "");
    }
}
