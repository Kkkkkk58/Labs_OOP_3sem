using System.Globalization;
using Banks.BankAccounts.Abstractions;
using Banks.BankAccountWrappers;
using Banks.Entities;
using Banks.Entities.Abstractions;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Models;

public class BankAccountFactory : IAccountFactory
{
    private readonly IClock _clock;
    private readonly Calendar _calendar;

    public BankAccountFactory(IClock clock, Calendar calendar)
    {
        _clock = clock;
        _calendar = calendar;
    }

    public IBankAccount CreateDebitAccount(
        IDebitAccountType type,
        ICustomer customer,
        MoneyAmount? balance = null)
    {
        MoneyAmount initialBalance = balance ?? new MoneyAmount(0);

        IBankAccount debitAccount = new BasicAccount(type, customer, initialBalance, _clock);
        debitAccount = new NonNegativeBalanceBankAccount(debitAccount);
        debitAccount = new InterestCalculationBankAccount(debitAccount, _clock, _calendar);
        debitAccount = new SuspiciousLimitingBankAccount(debitAccount);

        return debitAccount;
    }

    public IBankAccount CreateDepositAccount(
        IDepositAccountType type,
        ICustomer customer,
        MoneyAmount? balance = null)
    {
        MoneyAmount initialBalance = balance ?? new MoneyAmount(0);

        IBankAccount depositAccount = new BasicAccount(type, customer, initialBalance, _clock);
        depositAccount = new NonNegativeBalanceBankAccount(depositAccount);
        depositAccount = new InterestCalculationBankAccount(depositAccount, _clock, _calendar);
        depositAccount = new TimeLimitedWithdrawalBankAccount(depositAccount, _clock);
        depositAccount = new SuspiciousLimitingBankAccount(depositAccount);

        return depositAccount;
    }

    public IBankAccount CreateCreditAccount(
        ICreditAccountType type,
        ICustomer customer,
        MoneyAmount? balance = null)
    {
        MoneyAmount initialBalance = balance ?? new MoneyAmount(0);

        IBankAccount creditAccount = new BasicAccount(type, customer, initialBalance, _clock);
        creditAccount = new DebtLimitedBankAccount(creditAccount);
        creditAccount = new ChargeableAfterDebtBankAccount(creditAccount);
        creditAccount = new SuspiciousLimitingBankAccount(creditAccount);

        return creditAccount;
    }
}