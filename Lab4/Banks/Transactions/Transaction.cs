﻿using Banks.Commands.Abstractions;
using Banks.Models;
using Banks.Transactions.Abstractions;

namespace Banks.Transactions;

public class Transaction : ITransaction
{
    private readonly ICommand _command;
    private ITransactionState _state;

    public Transaction(OperationInformation information, ICommand command, ITransactionState? state = null)
    {
        Information = information;
        _command = command;
        _state = state ?? new NewTransactionState(this);
    }

    public OperationInformation Information { get; }

    public void ChangeState(ITransactionState state)
    {
        ArgumentNullException.ThrowIfNull(state);
        _state = state;
    }

    public void Perform()
    {
        _state.Perform(_command);
    }

    public void Cancel()
    {
        _state.Cancel(_command);
    }
}