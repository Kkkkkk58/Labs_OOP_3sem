using Isu.Models.Abstractions;
using Isu.Models.IsuInformationDetails;

namespace Isu.Models;

public class IsuIdGenerator : IIsuIdGenerator
{
    private int _nextId;

    public IsuIdGenerator(int firstId = 100_000)
    {
        _nextId = firstId;
    }

    public IsuId GetNext()
    {
        return new IsuId(_nextId++);
    }
}