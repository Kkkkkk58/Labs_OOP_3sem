using System.Globalization;
using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Commands;
using Banks.Entities.Abstractions;
using Banks.EventArgs;
using Banks.Exceptions;
using Banks.Models;
using Banks.Tools.Abstractions;
using Banks.Transactions.Abstractions;

namespace Banks.BankAccounts.Wrappers;

public class InterestCalculationBankAccount : BaseBankAccountWrapper, ISubscriber<DateChangedEventArgs>
{
    private readonly IInterestCalculationAccountType _type;
    private readonly Calendar _calendar;
    private MoneyAmount _savings;
    private DateTime _lastUpdate;
    private DateTime _date;

    public InterestCalculationBankAccount(IBankAccount wrapped, IClock clock, Calendar calendar)
        : base(wrapped)
    {
        ArgumentNullException.ThrowIfNull(wrapped);
        ArgumentNullException.ThrowIfNull(clock);
        ArgumentNullException.ThrowIfNull(calendar);
        if (wrapped.Type is not IInterestCalculationAccountType type)
            throw AccountWrapperException.InvalidWrappedType();

        _type = type;
        _calendar = calendar;
        _date = _lastUpdate = CreationDate;
        _savings = GetPercent(_date) * Balance;
        clock.Subscribe(Update);
    }

    public override void Replenish(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        base.Replenish(transaction);
        _savings += GetPercent(_date) * transaction.Information.OperatedAmount;
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        base.Withdraw(transaction);

        MoneyAmount moneyAmount = transaction.Information.OperatedAmount;
        _savings -= GetPercent(_date) * moneyAmount;

        return moneyAmount;
    }

    public void Update(object? sender, DateChangedEventArgs eventArgs)
    {
        ArgumentNullException.ThrowIfNull(eventArgs);
        if (eventArgs.Date < _date)
            throw InterestCalculationException.InvalidUpdateDate();

        _date = eventArgs.Date;
        int percentCalculationTimes = GetInterestCalculationTimes();
        if (percentCalculationTimes == 0)
            return;

        var operationInformation = new OperationInformation(this, _savings, _date);
        var transaction = new Transaction(operationInformation, new ReplenishmentCommand());
        AddPercents(percentCalculationTimes, transaction);
        _lastUpdate += percentCalculationTimes * _type.InterestCalculationPeriod;
    }

    private decimal GetPercent(DateTime date)
    {
        int days = _calendar.GetDaysInYear(date.Year);
        return _type.GetInterestPercent(InitialBalance) / days;
    }

    private void AddPercents(int percentCalculationTimes, ITransaction transaction)
    {
        for (int i = 1; i < percentCalculationTimes; ++i)
        {
            DateTime date = _lastUpdate.AddMonths(i);
            _savings *= 1 + GetPercent(date);
        }

        transaction.Information.SetOperatedAmount(_savings);
        transaction.Perform();
        transaction.ChangeState(new SuccessfulTransactionState(transaction));
    }

    private int GetInterestCalculationTimes()
    {
        return Convert.ToInt32(Math.Floor((_date - _lastUpdate) / _type.InterestCalculationPeriod));
    }
}