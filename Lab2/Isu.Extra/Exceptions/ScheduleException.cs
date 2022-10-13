namespace Isu.Extra.Exceptions;

public class ScheduleException : IsuExtraException
{
    public ScheduleException(string message)
        : base(message)
    {
    }

    public static ScheduleException ScheduleCombinationIntersects()
    {
        throw new ScheduleException("Can't combine schedules containing intersections");
    }
}