﻿using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Entities.Abstractions;

namespace Banks.Models.Abstractions;

public interface IAccountFactory
{
    IBankAccount CreateDebitAccount(IDebitAccountType type, ICustomer customer, MoneyAmount? balance = null);
    IBankAccount CreateDepositAccount(IDepositAccountType type, ICustomer customer, MoneyAmount? balance = null);
    IBankAccount CreateCreditAccount(ICreditAccountType type, ICustomer customer, MoneyAmount? balance = null);
}