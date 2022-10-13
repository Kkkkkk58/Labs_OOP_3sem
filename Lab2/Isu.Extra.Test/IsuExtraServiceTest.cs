using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Entities.LessonSchedulers;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Models.IsuInformationDetails;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraServiceTest
{
    private readonly IIsuExtraService _isuExtraService;
    private readonly DateOnly _scheduleStart;
    private readonly DateOnly _scheduleEnd;
    private readonly int _coursesLimit;

    public IsuExtraServiceTest()
    {
        _coursesLimit = 2;
        _scheduleStart = new DateOnly(2022, 9, 1);
        _scheduleEnd = new DateOnly(2023, 8, 1);
        _isuExtraService = GetNewService();
    }

    [Fact]
    public void AddNewExtraCourse_ExtraCourseAdded()
    {
        MegaFaculty mf = _isuExtraService.AddMegaFaculty(new MegaFaculty("TILT"));
        ExtraCourse course = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));

        Assert.Contains(course, _isuExtraService.ExtraCourses);
    }

    [Fact]
    public void SignUpStudentForExtraCourse_StudentDecoratorContainsChosenStream()
    {
        MegaFaculty mf = _isuExtraService.AddMegaFaculty(new MegaFaculty("TILT"));
        mf.AddFaculty(new Faculty("BBBB", new FacultyLetter('B')));
        ExtraCourse course = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));
        Schedule streamSchedule = GetSimpleSchedule(new DateTime(2022, 10, 14, 11, 40, 0));

        var stream = new ExtraStream("Stream", 12, course, streamSchedule);
        Group group = _isuExtraService.AddGroup(new GroupName("M32021"));
        Student student = _isuExtraService.AddStudent(group, "KSlacker");
        Schedule groupSchedule = GetDetailedSchedule();
        _isuExtraService.InitGroupSchedule(group, groupSchedule);

        _isuExtraService.SignUpStudentForExtraStream(student, stream);
        StudentDecorator sd = _isuExtraService.GetStudentDecorator(student);

        Assert.Contains(stream, sd.ExtraStreams);
        Assert.Contains(sd, stream.Students);
    }

    [Fact]
    public void ResetStudentExtraCourse_StudentDecoratorDoesNotContainChosenStream()
    {
        MegaFaculty mf = _isuExtraService.AddMegaFaculty(new MegaFaculty("TILT"));
        ExtraCourse course1 = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));
        ExtraCourse course2 = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "K"));
        Schedule stream1Schedule = GetSimpleSchedule(new DateTime(2022, 10, 14, 11, 40, 0));
        Schedule stream2Schedule = GetSimpleSchedule(new DateTime(2022, 10, 10, 13, 50, 0));
        var stream1 = new ExtraStream("Stream1", 12, course1, stream1Schedule);
        var stream2 = new ExtraStream("Stream2", 15, course2, stream2Schedule);
        Group group = _isuExtraService.AddGroup(new GroupName("M32021"));
        Student student = _isuExtraService.AddStudent(group, "KSlacker");
        Schedule groupSchedule = GetDetailedSchedule();
        _isuExtraService.InitGroupSchedule(group, groupSchedule);

        _isuExtraService.SignUpStudentForExtraStream(student, stream1);
        _isuExtraService.SignUpStudentForExtraStream(student, stream2);
        _isuExtraService.ResetStudentExtraStream(student, stream1);
        StudentDecorator sd = _isuExtraService.GetStudentDecorator(student);

        Assert.DoesNotContain(stream1, sd.ExtraStreams);
        Assert.DoesNotContain(sd, stream1.Students);
        Assert.Contains(stream2, sd.ExtraStreams);
    }

    [Fact]
    public void GetUnassignedStudentsByGroup_ReturnStudentsWithNotFullSetOfCourses()
    {
        MegaFaculty mf = _isuExtraService.AddMegaFaculty(new MegaFaculty("TILT"));
        ExtraCourse course1 = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));
        ExtraCourse course2 = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "K"));
        Schedule stream1Schedule = GetSimpleSchedule(new DateTime(2022, 10, 14, 11, 40, 0));
        Schedule stream2Schedule = GetSimpleSchedule(new DateTime(2022, 10, 10, 13, 50, 0));
        var stream1 = new ExtraStream("Stream1", 12, course1, stream1Schedule);
        var stream2 = new ExtraStream("Stream2", 15, course2, stream2Schedule);

        Group group = _isuExtraService.AddGroup(new GroupName("M32021"));
        Student student1 = _isuExtraService.AddStudent(group, "KSlacker");
        Student student2 = _isuExtraService.AddStudent(group, "Joseph Craig");
        Schedule groupSchedule = GetDetailedSchedule();
        _isuExtraService.InitGroupSchedule(group, groupSchedule);

        _isuExtraService.SignUpStudentForExtraStream(student1, stream1);
        _isuExtraService.SignUpStudentForExtraStream(student2, stream1);
        _isuExtraService.SignUpStudentForExtraStream(student2, stream2);
        StudentDecorator sd1 = _isuExtraService.GetStudentDecorator(student1);
        StudentDecorator sd2 = _isuExtraService.GetStudentDecorator(student2);

        Assert.Contains(sd1, _isuExtraService.GetUnassignedStudents(group));
        Assert.DoesNotContain(sd2, _isuExtraService.GetUnassignedStudents(group));
    }

    [Fact]
    public void AddStudentToFullExtraStream_ThrowException()
    {
        MegaFaculty mf = _isuExtraService.AddMegaFaculty(new MegaFaculty("TILT"));
        ExtraCourse course = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));
        Schedule streamSchedule = GetSimpleSchedule(new DateTime(2022, 10, 14, 11, 40, 0));

        var stream = new ExtraStream("Stream", 1, course, streamSchedule);
        Group group = _isuExtraService.AddGroup(new GroupName("M32021"));
        Student student1 = _isuExtraService.AddStudent(group, "KSlacker");
        Student student2 = _isuExtraService.AddStudent(group, "Yo Landi");
        Schedule groupSchedule = GetDetailedSchedule();
        _isuExtraService.InitGroupSchedule(group, groupSchedule);

        _isuExtraService.SignUpStudentForExtraStream(student1, stream);
        Assert.Throws<ExtraStreamException>(() => _isuExtraService.SignUpStudentForExtraStream(student2, stream));
    }

    [Fact]
    public void SignUpForTheSameMegaFacultyExtraCourse_ThrowException()
    {
        MegaFaculty mf = _isuExtraService.AddMegaFaculty(new MegaFaculty("TILT"));
        mf.AddFaculty(new Faculty("FITP", new FacultyLetter('M')));
        ExtraCourse course = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));
        Schedule streamSchedule = GetSimpleSchedule(new DateTime(2022, 10, 14, 11, 40, 0));

        var stream = new ExtraStream("Stream", 12, course, streamSchedule);
        Group group = _isuExtraService.AddGroup(new GroupName("M32021"));
        Student student = _isuExtraService.AddStudent(group, "KSlacker");
        Schedule groupSchedule = GetDetailedSchedule();
        _isuExtraService.InitGroupSchedule(group, groupSchedule);

        Assert.Throws<StudentDecoratorException>(() => _isuExtraService.SignUpStudentForExtraStream(student, stream));
    }

    [Fact]
    public void SchedulesHaveIntersections_ThrowException()
    {
        MegaFaculty mf = _isuExtraService.AddMegaFaculty(new MegaFaculty("TILT"));
        ExtraCourse course = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));
        Schedule streamSchedule = GetSimpleSchedule(new DateTime(2022, 10, 15, 11, 40, 0));

        var stream = new ExtraStream("Stream", 12, course, streamSchedule);
        Group group = _isuExtraService.AddGroup(new GroupName("M32021"));
        Student student = _isuExtraService.AddStudent(group, "KSlacker");
        Schedule groupSchedule = GetDetailedSchedule();
        _isuExtraService.InitGroupSchedule(group, groupSchedule);

        Assert.Throws<StudentDecoratorException>(() => _isuExtraService.SignUpStudentForExtraStream(student, stream));
    }

    [Fact]
    public void SignUpToMoreThanNeededCourses_ThrowException()
    {
        MegaFaculty mf = _isuExtraService.AddMegaFaculty(new MegaFaculty("TILT"));
        ExtraCourse course1 = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));
        ExtraCourse course2 = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));
        ExtraCourse course3 = _isuExtraService.AddExtraCourse(new ExtraCourse(mf, "SE"));
        Schedule stream1Schedule = GetSimpleSchedule(new DateTime(2022, 10, 14, 11, 40, 0));
        Schedule stream2Schedule = GetSimpleSchedule(new DateTime(2022, 9, 12, 11, 40, 0));
        Schedule stream3Schedule = GetSimpleSchedule(new DateTime(2023, 05, 07, 8, 20, 0));

        var stream1 = new ExtraStream("Stream1", 12, course1, stream1Schedule);
        var stream2 = new ExtraStream("Stream2", 9, course2, stream2Schedule);
        var stream3 = new ExtraStream("Stream3", 50, course3, stream3Schedule);
        Group group = _isuExtraService.AddGroup(new GroupName("M32021"));
        Student student = _isuExtraService.AddStudent(group, "KSlacker");
        Schedule groupSchedule = GetDetailedSchedule();
        _isuExtraService.InitGroupSchedule(group, groupSchedule);

        _isuExtraService.SignUpStudentForExtraStream(student, stream1);
        _isuExtraService.SignUpStudentForExtraStream(student, stream2);

        Assert.Throws<StudentDecoratorException>(() => _isuExtraService.SignUpStudentForExtraStream(student, stream3));
    }

    private static IIsuService GetOldService()
    {
        var options = new IsuServiceOptions(20);
        var idGenerator = new IsuIdGenerator();
        return new IsuService(options, idGenerator);
    }

    private IIsuExtraService GetNewService()
    {
        IIsuService oldService = GetOldService();
        var options = new IsuExtraServiceOptions(_coursesLimit);
        return new IsuExtraService(options, oldService);
    }

    private Schedule GetSimpleSchedule(DateTime lessonDate)
    {
        var time = new LessonTime(
            lessonDate,
            TimeSpan.FromMinutes(90));

        ILessonScheduler scheduler = new SingleTimeLessonScheduler();
        var lesson = new Lesson("English", time, new Teacher("A"), new ClassRoomLocation("Anywhere"));
        ILessonSchedulingOptionsBuilder optionsBuilder = new LessonSchedulingOptionsBuilder();

        return Schedule
            .Builder
            .SetStartDate(_scheduleStart)
            .SetEndDate(_scheduleEnd)
            .AddLesson(scheduler, optionsBuilder.SetLessonRepeatNumber(1).SetLesson(lesson))
            .Build();
    }

    private Schedule GetDetailedSchedule()
    {
        var time1 = new LessonTime(new DateTime(2022, 10, 15, 11, 40, 0), TimeSpan.FromMinutes(90));
        var time2 = new LessonTime(new DateTime(2022, 10, 17, 8, 20, 0), TimeSpan.FromMinutes(90));

        ILessonScheduler scheduler = new EveryNWeeksLessonScheduler(2);
        var lesson1 = new Lesson("English", time1, new Teacher("A"), new ClassRoomLocation("Anywhere"));
        var lesson2 = new Lesson("Linear algebra", time2, new Teacher("B"), new ClassRoomLocation("Somewhere"));
        ILessonSchedulingOptionsBuilder optionsBuilder = new LessonSchedulingOptionsBuilder();

        return Schedule
            .Builder
            .SetStartDate(_scheduleStart)
            .SetEndDate(_scheduleEnd)
            .AddLesson(scheduler, optionsBuilder.SetLesson(lesson1).SetLessonRepeatNumber(10))
            .AddLesson(scheduler, optionsBuilder.SetLesson(lesson2).SetLessonRepeatNumber(14))
            .Build();
    }
}