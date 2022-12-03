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
    void Withdraw(Guid accountId, MoneyAmount moneyAmount);
    void Replenish(Guid accountId, MoneyAmount moneyAmount);
    void Transfer(Guid fromAccountId, Guid toAccountId, MoneyAmount moneyAmount);
}