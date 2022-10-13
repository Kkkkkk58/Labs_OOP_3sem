namespace Isu.Extra.Models;

public struct IsuExtraServiceOptions
{
    public IsuExtraServiceOptions(int extraStreamsLimit)
    {
        if (extraStreamsLimit < 0)
            throw new ArgumentOutOfRangeException(nameof(extraStreamsLimit));

        ExtraStreamsLimit = extraStreamsLimit;
    }

    public int ExtraStreamsLimit { get; }
}