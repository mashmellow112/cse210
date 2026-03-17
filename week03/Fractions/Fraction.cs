/// <summary>
/// Represents a fraction with a numerator (top) and denominator (bottom).
/// </summary>
public class Fraction
{
    // Private attributes for top and bottom numbers
    private int _top;
    private int _bottom;

    // Constructor with no parameters - initializes to 1/1
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    // Constructor with one parameter for top - initializes denominator to 1
    public Fraction(int top)
    {
        _top = top;
        _bottom = 1;
    }

    // Constructor with two parameters, one for top and one for bottom
    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }

    // Getter and Setter for the top number (numerator)
    public int GetTop()
    {
        return _top;
    }

    public void SetTop(int top)
    {
        _top = top;
    }

    // Getter and Setter for the bottom number (denominator)
    public int GetBottom()
    {
        return _bottom;
    }

    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }

    // Returns the fraction as a string in the form "top/bottom"
    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    // Returns the decimal value of the fraction
    public double GetDecimalValue()
    {
        return (double)_top / (double)_bottom;
    }
}
