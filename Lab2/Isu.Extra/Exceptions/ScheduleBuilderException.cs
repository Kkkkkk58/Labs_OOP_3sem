namespace Isu.Extra.Exceptions;

public class ScheduleBuilderException : IsuExtraException
{
    private ScheduleBuilderException(string message)
        : base(message)
    {
    }

    public static ScheduleBuilderException NoTimeLimitsProvided()
    {
        throw new ScheduleBuilderException("To add lesson, provide builder with dates of end and start of the period");
    }

    public static ScheduleBuilderException StartDateExceedsEndDate(DateOnly startDate, DateOnly endDate)
    {
        throw new ScheduleBuilderException($"Start date {startDate} can't exceed the end date {endDate}");
    }

    public static ScheduleBuilderException EndDatePrecedesStartDate(DateOnly endDate, DateOnly startDate)
    {
        throw new ScheduleBuilderException($"End date {endDate} can't precede the start date {startDate}");
    }
}