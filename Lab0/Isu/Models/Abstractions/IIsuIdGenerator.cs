using Isu.Models.IsuInformationDetails;

namespace Isu.Models.Abstractions;

public interface IIsuIdGenerator
{
    IsuId GetNext();
}