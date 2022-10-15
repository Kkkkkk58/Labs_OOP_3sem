namespace Isu.Extra.Extensions;

public static class DateOnlyExtensions
{
    public static DateTime ToDateTimeWithSameTime(this DateOnly a, DateTime b)
    {
        return a.ToDateTime(new TimeOnly(b.Hour, b.Minute, b.Second, b.Millisecond));
    }

    public static int GetDifferenceInWeeks(this DateOnly a, DateTime b)
    {
        return (int)Math.Abs((a.ToDateTimeWithSameTime(b) - b).TotalDays) / 7;
    }
}