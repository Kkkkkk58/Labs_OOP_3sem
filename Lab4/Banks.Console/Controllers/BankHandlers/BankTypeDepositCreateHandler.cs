﻿using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Entities;
using Banks.Entities.Abstractions;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeDepositCreateHandler : Handler
{
    private readonly AppContext _context;
    public BankTypeDepositCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));

        var interestOnBalancePolicy = new InterestOnBalancePolicy();
        System.Console.Write("Enter layer: ");
        string input = string.Empty;
        while ((input = System.Console.ReadLine() ?? throw new NotImplementedException()) != "\n")
        {
            string[] values = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var requiredBalance = values[0].ToMoneyAmount();
            decimal interest = decimal.Parse(values[1]);
            var layer = new InterestOnBalanceLayer(requiredBalance, interest);
            interestOnBalancePolicy.AddLayer(layer);
        }

        System.Console.Write("Enter deposit term: ");
        var term = TimeSpan.Parse(System.Console.ReadLine() ?? throw new NotImplementedException());
        System.Console.Write("Enter interest calculation period: ");
        var period = TimeSpan.Parse(System.Console.ReadLine() ?? throw new NotImplementedException());

        IDepositAccountType type = bank.AccountTypeManager.CreateDepositAccountType(term, interestOnBalancePolicy, period);

        // TODO
        System.Console.WriteLine(type.Id);
        base.Handle(args);
    }
}