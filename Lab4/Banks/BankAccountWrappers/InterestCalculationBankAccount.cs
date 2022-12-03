using System.Globalization;
using Banks.BankAccounts.Abstractions;
using Banks.Commands;
using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.BankAccountWrappers;

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
        if (wrapped.Type is not IInterestCalculationAccountType type)
            throw new NotImplementedException();

        _type = type;
        _calendar = calendar;
        _date = _lastUpdate = CreationDate;
        _savings = GetPercent(_date) * Balance;
        clock.Subscribe(Update);
    }

    public override void Replenish(ITransaction transaction)
    {
        base.Replenish(transaction);
        _savings += GetPercent(_date) * transaction.Information.OperatedAmount;
    }

    // TODO find problems with _date month in days and subtract from creation date not from last update
    // TODO Get custom period in ctor
    public void Update(object? sender, DateChangedEventArgs eventArgs)
    {
        if (eventArgs.Date < _date)
            throw new NotImplementedException();
        _date = eventArgs.Date;
        int percentCalculationTimes = GetInterestCalculationTimes();
        if (percentCalculationTimes == 0)
            return;

        var transaction = new Transaction(
            new OperationInformation(this, _savings, _date, "//TODO"),
            new ReplenishmentCommand());
        AddPercents(percentCalculationTimes, transaction);
        _lastUpdate = _lastUpdate.AddMonths(percentCalculationTimes);
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        base.Withdraw(transaction);

        MoneyAmount moneyAmount = transaction.Information.OperatedAmount;
        _savings -= GetPercent(_date) * moneyAmount;

        return moneyAmount;
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