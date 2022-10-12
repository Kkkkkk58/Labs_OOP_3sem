namespace Isu.Extra.Models;

public record struct ClassRoomNumber
{
    public ClassRoomNumber(int value)
    {
        if (value <= 0)
            throw new NotImplementedException();

        Value = value;
    }

    public int Value { get; }
}