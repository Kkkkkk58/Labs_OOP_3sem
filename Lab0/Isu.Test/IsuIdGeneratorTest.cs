using Isu.Models;
using Isu.Models.IsuInformationDetails;
using Xunit;

namespace Isu.Test;

public class IsuIdGeneratorTest
{
    private const int MaxSteps = 15;

    [Fact]
    public void CreatingNewId_ReceiveIncrementedValue()
    {
        var idGenerator = new IsuIdGenerator(337031);
        IsuId nextId = idGenerator.GetNext();

        int steps = GetRandomStepsValue();
        for (int i = 0; i < steps; ++i)
        {
            IsuId prevId = nextId;
            nextId = idGenerator.GetNext();
            Assert.Equal(prevId.Value + 1, nextId.Value);
        }
    }

    private static int GetRandomStepsValue()
    {
        var rnd = new Random();
        int suitableValue = (rnd.Next() % MaxSteps) + 1;
        return suitableValue;
    }
}