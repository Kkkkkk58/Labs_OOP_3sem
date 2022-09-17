using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Models.IsuInformationDetails;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        IsuService isuService = SetUpIsuService();
        Group group = isuService.AddGroup(new GroupName("M3102"));
        Student student = isuService.AddStudent(group, "Kracker Slacker");
        Assert.Contains(student, group.Students);
        Assert.True(student.Group.Equals(group));
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        IsuService isuService = SetUpIsuService(new IsuServiceOptions(1));
        Group group = isuService.AddGroup(new GroupName("M32021"));
        isuService.AddStudent(group, "NASTYA ADMIN");

        Assert.Throws<GroupException>(() => isuService.AddStudent(group, "SUREN ADMIN"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("M310")]
    [InlineData("M320211")]
    [InlineData("03102")]
    [InlineData("MM102")]
    [InlineData("M9102")]
    [InlineData("M3M02")]
    [InlineData("M3502")]
    [InlineData("M31M2")]
    [InlineData("M310M")]
    [InlineData("M3102m")]
    public void CreateGroupWithInvalidName_ThrowException(string invalidName)
    {
        IsuService isuService = SetUpIsuService();
        Assert.Throws<InvalidIsuInformationException>(() => isuService.AddGroup(new GroupName(invalidName)));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        IsuService isuService = SetUpIsuService();
        Group oldGroup = isuService.AddGroup(new GroupName("M3102"));
        Student student = isuService.AddStudent(oldGroup, "Artyom Shvetsov");
        Assert.True(student.Group.Equals(oldGroup));

        Group newGroup = isuService.AddGroup(new GroupName("M3134"));
        isuService.ChangeStudentGroup(student, newGroup);

        Assert.True(student.Group.Equals(newGroup));
        Assert.Contains(student, newGroup.Students);
        Assert.DoesNotContain(student, oldGroup.Students);
    }

    [Fact]
    public void GetExistingStudentById_ReturnStudent()
    {
        IsuService isuService = SetUpIsuService();
        Group group = isuService.AddGroup(new GroupName("R3435"));
        Student createdStudent = isuService.AddStudent(group, "John Doe");

        Student receivedStudent = isuService.GetStudent(createdStudent.PersonalInfo.Id.Value);
        Assert.Equal(createdStudent, receivedStudent);
    }

    [Fact]
    public void GetNotExistingStudentById_ThrowException()
    {
        IsuService isuService = SetUpIsuService();
        Assert.Throws<StudentException>(() => isuService.GetStudent(337031));
    }

    [Fact]
    public void FindExistingStudentById_ReturnStudent()
    {
        IsuService isuService = SetUpIsuService();
        Group group = isuService.AddGroup(new GroupName("R3435"));
        Student createdStudent = isuService.AddStudent(group, "John Doe");

        Student? receivedStudent = isuService.FindStudent(createdStudent.PersonalInfo.Id.Value);
        Assert.NotNull(receivedStudent);
    }

    [Fact]
    public void FindNotExistingStudentById_ReturnNull()
    {
        IsuService isuService = SetUpIsuService();
        Student? student = isuService.FindStudent(337031);
        Assert.Null(student);
    }

    [Fact]
    public void FindStudentsByGroupName_ReturnListContainingStudentsFromGroup()
    {
        IsuService isuService = SetUpIsuService();
        SeedWithGroupsAndStudents(isuService);

        var groupName = new GroupName("M32001");
        IReadOnlyList<Student> students = isuService.FindStudents(groupName);
        bool condition = students.All(student => student.Group.Name.Equals(groupName));
        Assert.True(condition);
    }

    [Fact]
    public void FindStudentsByCourseNumber_ReturnListContainingStudentsFromCourse()
    {
        IsuService isuService = SetUpIsuService();
        SeedWithGroupsAndStudents(isuService);

        var courseNumber = new CourseNumber(2);
        IReadOnlyList<Student> students = isuService.FindStudents(courseNumber);
        bool condition = students.All(student => student.Group.Name.Details.CourseNumber.Equals(courseNumber));
        Assert.True(condition);
    }

    [Fact]
    public void FindExistingGroupByName_ReturnGroup()
    {
        IsuService isuService = SetUpIsuService();
        var groupName = new GroupName("M34002");
        isuService.AddGroup(groupName);

        Group? receivedGroup = isuService.FindGroup(groupName);
        Assert.NotNull(receivedGroup);
    }

    [Fact]
    public void FindNotExistingGroupByName_ReturnNull()
    {
        IsuService isuService = SetUpIsuService();
        var groupName = new GroupName("M34002");

        Group? receivedGroup = isuService.FindGroup(groupName);
        Assert.Null(receivedGroup);
    }

    [Fact]
    public void FindGroupsByCourseNumber_ReturnListContainingGroupsFromCourse()
    {
        IsuService isuService = SetUpIsuService();
        SeedWithGroupsAndStudents(isuService);

        var courseNumber = new CourseNumber(2);
        IReadOnlyList<Group> groups = isuService.FindGroups(courseNumber);
        bool condition = groups.All(group => group.Name.Details.CourseNumber.Equals(courseNumber));
        Assert.True(condition);
    }

    private static IsuService SetUpIsuService(IsuServiceOptions options = default)
    {
        var idGenerator = new IsuIdGenerator();
        return new IsuService(options, idGenerator);
    }

    private static void SeedWithGroupsAndStudents(IIsuService isuService)
    {
        Group group1 = isuService.AddGroup(new GroupName("M32001"));
        Group group2 = isuService.AddGroup(new GroupName("M3102"));
        Group group3 = isuService.AddGroup(new GroupName("U3207"));
        Group group4 = isuService.AddGroup(new GroupName("R3435"));

        isuService.AddStudent(group1, "Oleg Zhelikhovsky");
        isuService.AddStudent(group1, "Nikolai Novikov");

        isuService.AddStudent(group2, "Vladislav Frolov");
        isuService.AddStudent(group2, "Vladislav Dubrovin");

        isuService.AddStudent(group3, "Gennady Abashkin");
        isuService.AddStudent(group3, "Egor Stulnikov");

        isuService.AddStudent(group4, "Kilgore Trout");
        isuService.AddStudent(group4, "Alan Wake");
    }
}