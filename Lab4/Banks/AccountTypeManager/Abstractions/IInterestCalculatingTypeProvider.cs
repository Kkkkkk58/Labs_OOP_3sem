namespace Banks.AccountTypeManager.Abstractions;

public interface IInterestCalculatingTypeProvider
{
    void ChangeInterestCalculationPeriod(Guid typeId, TimeSpan interestCalculationPeriod);
}