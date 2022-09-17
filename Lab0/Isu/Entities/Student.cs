using Isu.Exceptions;
using Isu.Models.IsuInformationDetails;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    public Student(PersonalInformation personalInfo, Group group)
    {
        PersonalInfo = personalInfo;
        Group = group;

        Group.AddStudent(this);
    }

    public PersonalInformation PersonalInfo { get; }

    public Group Group { get; private set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Student);
    }

    public bool Equals(Student? other)
    {
        return other is not null
               && PersonalInfo.Id.Equals(other.PersonalInfo.Id);
    }

    public override int GetHashCode()
    {
        return PersonalInfo.Id.Value;
    }

    public void ChangeGroup(Group newGroup)
    {
        if (Group.Equals(newGroup))
            throw StudentException.SameGroupTransfer(PersonalInfo.Id, newGroup.Name);

        newGroup.AddStudent(this);
        Group.RemoveStudent(this);
        Group = newGroup;
    }
}