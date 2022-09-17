using Isu.Exceptions;

namespace Isu.Models.IsuInformationDetails;

public readonly struct PersonalInformation
{
    public PersonalInformation(IsuId id, string name)
    {
        if (string.IsNullOrEmpty(name))
            throw InvalidIsuInformationException.EmptyNameOfPerson();

        Id = id;
        Name = name;
    }

    public IsuId Id { get; }
    public string Name { get; }
}