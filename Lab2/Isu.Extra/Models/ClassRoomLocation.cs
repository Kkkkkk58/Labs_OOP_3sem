using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public record struct ClassRoomLocation
{
    private readonly string _value;

    public ClassRoomLocation(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw ClassRoomLocationException.EmptyValue();

        _value = value;
    }
}