using Banks.AccountTypes.Abstractions;

namespace Banks.AccountTypeManager.Abstractions;

public interface IAccountTypeManager : IDebitTypeProvider, ICreditTypeProvider, IDepositTypeProvider
{
    IAccountType GetAccountType(Guid id);
}