using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;

namespace Banks.Services.Abstractions;

public interface ICentralBank
{
    IReadOnlyCollection<INoTransactionalBank> Banks { get; }
    IReadOnlyCollection<IOperationInformation> Operations { get; }
    INoTransactionalBank RegisterBank(IBank bank);
    void CancelTransaction(Guid transactionId);
    IOperationInformation Withdraw(Guid accountId, MoneyAmount moneyAmount);
    IOperationInformation Replenish(Guid accountId, MoneyAmount moneyAmount);
    IOperationInformation Transfer(Guid fromAccountId, Guid toAccountId, MoneyAmount moneyAmount);
}