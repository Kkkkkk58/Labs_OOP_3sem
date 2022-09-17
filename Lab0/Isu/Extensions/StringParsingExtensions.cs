namespace Isu.Extensions;

public static class StringParsingExtensions
{
    public static bool TryTransformToNum(this string s, out int number)
    {
        return int.TryParse(s, out number);
    }

    public static bool TryTransformToLetter(this string s, out char letter)
    {
        return char.TryParse(s, out letter)
               && char.IsLetter(letter);
    }
}