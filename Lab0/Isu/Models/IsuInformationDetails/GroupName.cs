using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Models.IsuInformationDetails;

public record GroupName
{
    private static readonly Regex GroupNamePattern =
        new(@"^[A-Z]\d{4,5}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public GroupName(string name)
    {
        if (IsInvalidGroupName(name))
            throw InvalidIsuInformationException.InvalidGroupName(name);

        Value = name.ToUpperInvariant();
        Details = GroupNameDetails.Parse(Value);
    }

    public string Value { get; }
    public GroupNameDetails Details { get; }

    public override string ToString()
    {
        return Value;
    }

    private static bool IsInvalidGroupName(string name)
    {
        return !GroupNamePattern.IsMatch(name);
    }
}