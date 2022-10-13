namespace Isu.Extra.Models;

public struct IsuExtraServiceOptions
{
    public IsuExtraServiceOptions(int studentExtraCoursesLimit)
    {
        if (studentExtraCoursesLimit < 0)
            throw new ArgumentOutOfRangeException(nameof(studentExtraCoursesLimit));

        StudentExtraCoursesLimit = studentExtraCoursesLimit;
    }

    public int StudentExtraCoursesLimit { get; }
}