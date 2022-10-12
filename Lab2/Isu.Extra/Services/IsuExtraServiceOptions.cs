namespace Isu.Extra.Services;

public struct IsuExtraServiceOptions
{
    public IsuExtraServiceOptions(int extraStreamsLimit)
    {
        ExtraStreamsLimit = extraStreamsLimit;
    }

    public int ExtraStreamsLimit { get; }
}