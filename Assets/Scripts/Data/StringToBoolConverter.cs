using System;

public static class StringToBoolConverter
{
    public static bool Convert(string value)
    {
        if (string.Equals(value, "TRUE", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        else if (string.Equals(value, "FALSE", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }
        else
        {
            throw new ArgumentException($"Invalid string value: {value}");
        }
    }
}
