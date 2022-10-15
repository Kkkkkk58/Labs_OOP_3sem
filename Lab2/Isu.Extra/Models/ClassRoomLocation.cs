namespace Isu.Extra.Models;

public record struct ClassRoomLocation
{
    private readonly string _value;

    public ClassRoomLocation(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        _value = value;
    }
}