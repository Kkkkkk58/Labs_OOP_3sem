using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.AccountTypes;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Entities;

public class AccountTypeManager : IAccountTypeManager
{
    private readonly List<IAccountType> _types;
    private readonly EventHandler<BankTypeChangesEventArgs> _updateHandler;

    public AccountTypeManager(MoneyAmount suspiciousOperationsLimit, Action<object?, BankTypeChangesEventArgs> updater)
    {
        _types = new List<IAccountType>();
        SuspiciousAccountsOperationsLimit = suspiciousOperationsLimit;
        _updateHandler += (sender, args) => updater(sender, args);
    }

    public MoneyAmount SuspiciousAccountsOperationsLimit { get; private set; }

    public void ChangeInterestOnBalance(Guid debitTypeId, decimal newInterestOnBalance)
    {
        if (newInterestOnBalance < 0)
            throw new NotImplementedException();
        if (GetAccountType(debitTypeId) is not IDebitAccountType debitType)
            throw new NotImplementedException();

        debitType.SetInterestPercent(newInterestOnBalance);

        // TODO Static methods or classes instead of pure strings
        _updateHandler.Invoke(this, new BankTypeChangesEventArgs(debitType, $"New interest on balance is {newInterestOnBalance}"));
    }

    public IDebitAccountType CreateDebitAccountType(decimal interestOnBalance, TimeSpan interestCalculationPeriod)
    {
        IDebitAccountType type = new DebitAccountType(interestOnBalance, interestCalculationPeriod, SuspiciousAccountsOperationsLimit);
        _types.Add(type);

        return type;
    }

    public void ChangeDebtLimit(Guid creditTypeId, MoneyAmount newCreditLimit)
    {
        if (newCreditLimit.Value < 0)
            throw new NotImplementedException();
        if (GetAccountType(creditTypeId) is not ICreditAccountType creditType)
            throw new NotImplementedException();

        creditType.SetDebtLimit(newCreditLimit);

        _updateHandler.Invoke(this, new BankTypeChangesEventArgs(creditType, $"New credit limit is {newCreditLimit}"));
    }

    public void ChangeCreditCharge(Guid creditTypeId, MoneyAmount newCharge)
    {
        if (newCharge.Value < 0)
            throw new NotImplementedException();
        if (GetAccountType(creditTypeId) is not ICreditAccountType creditType)
            throw new NotImplementedException();

        creditType.SetCharge(newCharge);
        _updateHandler.Invoke(this, new BankTypeChangesEventArgs(creditType, $"New credit charge is {newCharge}"));
    }

    public ICreditAccountType CreateCreditAccountType(MoneyAmount debtLimit, MoneyAmount charge)
    {
        ICreditAccountType type = new CreditAccountType(debtLimit, charge, SuspiciousAccountsOperationsLimit);
        _types.Add(type);

        return type;
    }

    public void ChangeInterestOnBalanceLayer(Guid depositTypeId, Guid layerToSubstituteId, InterestOnBalanceLayer newLayer)
    {
        if (GetAccountType(depositTypeId) is not IDepositAccountType type)
            throw new NotImplementedException();

        // TODO Better layer management
        InterestOnBalanceLayer layer =
            type.InterestOnBalancePolicy.Layers.Single(layer => layer.Id.Equals(layerToSubstituteId));

        type.RemoveLayer(layer);
        type.AddLayer(newLayer);

        _updateHandler.Invoke(this, new BankTypeChangesEventArgs(type, "Substituted interest on balance layer"));
    }

    public void ChangeDepositTerm(Guid depositTypeId, TimeSpan newDepositTerm)
    {
        if (GetAccountType(depositTypeId) is not IDepositAccountType type)
            throw new NotImplementedException();

        type.SetDepositTerm(newDepositTerm);
        _updateHandler.Invoke(this, new BankTypeChangesEventArgs(type, $"New deposit term is {newDepositTerm}"));
    }

    public IDepositAccountType CreateDepositAccountType(TimeSpan depositTerm, InterestOnBalancePolicy interestOnBalancePolicy, TimeSpan interestCalculationPeriod)
    {
        IDepositAccountType type = new DepositAccountType(interestOnBalancePolicy, depositTerm, interestCalculationPeriod, SuspiciousAccountsOperationsLimit);
        _types.Add(type);

        return type;
    }

    public void ChangeInterestCalculationPeriod(Guid accountId, TimeSpan interestCalculationPeriod)
    {
        throw new NotImplementedException();
    }

    public void SetSuspiciousOperationsLimit(MoneyAmount limit)
    {
        if (limit.Value < 0)
            throw new NotImplementedException();

        SuspiciousAccountsOperationsLimit = limit;
        var suspiciousLimitingAccountTypes = new List<ISuspiciousLimitingAccountType>();
        foreach (IAccountType accountType in _types)
        {
            if (accountType is not ISuspiciousLimitingAccountType type)
                continue;

            type.SetSuspiciousAccountsOperationsLimit(SuspiciousAccountsOperationsLimit);
            suspiciousLimitingAccountTypes.Add(type);
        }

        foreach (ISuspiciousLimitingAccountType type in suspiciousLimitingAccountTypes)
        {
            _updateHandler.Invoke(this, new BankTypeChangesEventArgs(type, $"Updated suspicious operations policy: limit is {limit}"));
        }
    }

    public IAccountType GetAccountType(Guid id)
    {
        return _types.Single(type => type.Id.Equals(id));
    }
}