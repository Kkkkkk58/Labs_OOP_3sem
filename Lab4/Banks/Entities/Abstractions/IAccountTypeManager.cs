using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Entities.Abstractions;

public interface IAccountTypeManager : IDebitTypeProvider, ICreditTypeProvider, IDepositTypeProvider
{
    IAccountType GetAccountType(Guid id);
}