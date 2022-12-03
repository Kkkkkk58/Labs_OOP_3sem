using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities;
using Banks.Entities.Abstractions;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDepositCreateHandler : Handler
{
    private readonly AppContext _context;
    public BankTypeDepositCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));

        var interestOnBalancePolicy = new InterestOnBalancePolicy();
        _context.Writer.Write("Enter layer: ");
        string input = string.Empty;
        while ((input = _context.Reader.ReadLine()) != "\n")
        {
            string[] values = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var requiredBalance = values[0].ToMoneyAmount();
            decimal interest = decimal.Parse(values[1]);
            var layer = new InterestOnBalanceLayer(requiredBalance, interest);
            interestOnBalancePolicy.AddLayer(layer);
        }

        _context.Writer.Write("Enter deposit term: ");
        var term = TimeSpan.Parse(_context.Reader.ReadLine());
        _context.Writer.Write("Enter interest calculation period: ");
        var period = TimeSpan.Parse(_context.Reader.ReadLine());

        IDepositAccountType type = bank.AccountTypeManager.CreateDepositAccountType(term, interestOnBalancePolicy, period);

        _context.Writer.WriteLine($"Successfully created deposit type {type.Id}");
    }
}