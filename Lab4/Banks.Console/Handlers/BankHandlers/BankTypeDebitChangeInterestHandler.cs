﻿using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDebitChangeInterestHandler : Handler
{
    private readonly AppContext _context;

    public BankTypeDebitChangeInterestHandler(AppContext context)
        : base("interest")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var bankId = args[1].ToGuid();
        var typeId = args[2].ToGuid();
        decimal interestOnBalance = decimal.Parse(args[3]);

        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        bank.AccountTypeManager.ChangeInterestOnBalance(typeId, interestOnBalance);
    }
}