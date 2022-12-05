using Banks.AccountTypeManager.Abstractions;
using Banks.AccountTypes;
using Banks.AccountTypes.Abstractions;
using Banks.EventArgs;
using Banks.Exceptions;
using Banks.Models;

namespace Banks.AccountTypeManager;

public class AccountTypeManager : IAccountTypeManager
{
    private readonly List<IAccountType> _types;
    private readonly EventHandler<BankTypeChangesEventArgs> _updateHandler;

    public AccountTypeManager(MoneyAmount suspiciousOperationsLimit, Action<object?, BankTypeChangesEventArgs> updater)
    {
        ArgumentNullException.ThrowIfNull(updater);

        _types = new List<IAccountType>();
        SuspiciousAccountsOperationsLimit = suspiciousOperationsLimit;
        _updateHandler += (sender, args) => updater(sender, args);
    }

    public MoneyAmount SuspiciousAccountsOperationsLimit { get; private set; }

    public void ChangeInterestOnBalance(Guid debitTypeId, decimal newInterestOnBalance)
    {
        if (newInterestOnBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(newInterestOnBalance));
        if (GetAccountType(debitTypeId) is not IDebitAccountType debitType)
            throw AccountTypeManagerException.InvalidAccountType();

        debitType.SetInterestPercent(newInterestOnBalance);

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
        if (GetAccountType(creditTypeId) is not ICreditAccountType creditType)
            throw AccountTypeManagerException.InvalidAccountType();

        creditType.SetDebtLimit(newCreditLimit);

        _updateHandler.Invoke(this, new BankTypeChangesEventArgs(creditType, $"New credit limit is {newCreditLimit}"));
    }

    public void ChangeCreditCharge(Guid creditTypeId, MoneyAmount newCharge)
    {
        if (GetAccountType(creditTypeId) is not ICreditAccountType creditType)
            throw AccountTypeManagerException.InvalidAccountType();

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
            throw AccountTypeManagerException.InvalidAccountType();

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
            throw AccountTypeManagerException.InvalidAccountType();

        type.SetDepositTerm(newDepositTerm);
        _updateHandler.Invoke(this, new BankTypeChangesEventArgs(type, $"New deposit term is {newDepositTerm}"));
    }

    public IDepositAccountType CreateDepositAccountType(TimeSpan depositTerm, InterestOnBalancePolicy interestOnBalancePolicy, TimeSpan interestCalculationPeriod)
    {
        IDepositAccountType type = new DepositAccountType(interestOnBalancePolicy, depositTerm, interestCalculationPeriod, SuspiciousAccountsOperationsLimit);
        _types.Add(type);

        return type;
    }

    public void ChangeInterestCalculationPeriod(Guid typeId, TimeSpan interestCalculationPeriod)
    {
        if (GetAccountType(typeId) is not IInterestCalculationAccountType type)
            throw AccountTypeManagerException.InvalidAccountType();

        type.SetInterestCalculationPeriod(interestCalculationPeriod);
        _updateHandler.Invoke(
            this,
            new BankTypeChangesEventArgs(type, $"New interest calculation period is {interestCalculationPeriod}"));
    }

    public void SetSuspiciousOperationsLimit(MoneyAmount limit)
    {
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