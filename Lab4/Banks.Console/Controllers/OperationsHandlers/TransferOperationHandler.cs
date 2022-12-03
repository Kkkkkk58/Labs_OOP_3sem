﻿using Banks.Console.Chains;
using Banks.Console.Extensions;

namespace Banks.Console.Controllers.OperationsHandlers;

public class TransferOperationHandler : Handler
{
    private readonly AppContext _context;

    public TransferOperationHandler(AppContext context)
        : base("transfer")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var fromAccountId = args[1].ToGuid();
        var toAccountId = args[2].ToGuid();
        var moneyAmount = args[3].ToMoneyAmount();
        _context.CentralBank.Transfer(fromAccountId, toAccountId, moneyAmount);
        base.Handle(args);
    }
}