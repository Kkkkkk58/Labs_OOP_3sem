namespace Isu.Extra.Exceptions;

public class ScheduleBuilderException : IsuExtraException
{
    private ScheduleBuilderException(string message)
        : base(message)
    {
    }

    public static ScheduleBuilderException NoTimeLimitsProvided()
    {
        throw new ScheduleBuilderException($"To add lesson, provide builder with dates of end and start of the period");
    }
}