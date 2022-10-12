namespace Isu.Extra.Extensions;

public static class DateTimeExtensions
{
    public static int GetDifferenceInWeeks(this DateTime a, DateOnly b)
    {
        return (int)(a - b.ToDateTimeWithSameTime(a)).TotalDays / 7;
    }
}