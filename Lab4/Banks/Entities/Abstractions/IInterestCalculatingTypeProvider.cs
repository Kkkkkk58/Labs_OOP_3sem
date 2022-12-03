namespace Banks.Entities.Abstractions;

public interface IInterestCalculatingTypeProvider
{
    void ChangeInterestCalculationPeriod(Guid accountId, TimeSpan interestCalculationPeriod);
}