using Isu.Exceptions;

namespace Isu.Services;

public struct IsuServiceOptions
{
    public IsuServiceOptions(int? groupLimit = null)
    {
        if (groupLimit is < 0)
            throw InvalidIsuServiceOptionsException.NegativeGroupLimit(groupLimit.Value);

        GroupLimit = groupLimit;
    }

    public int? GroupLimit { get; }
}