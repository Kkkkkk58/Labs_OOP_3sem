using Banks.AccountTypes.Abstractions;
using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

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
        INoTransactionalBank bank = GetBank();
        IDepositAccountType type = GetType(bank);

        _context.Writer.WriteLine($"Successfully created deposit type {type.Id}");
    }

    private INoTransactionalBank GetBank()
    {
        _context.Writer.Write("Enter bank id: ");
        var bankId = _context.Reader.ReadLine().ToGuid();

        return _context
            .CentralBank
            .Banks
            .Single(b => b.Id.Equals(bankId));
    }

    private IDepositAccountType GetType(INoTransactionalBank bank)
    {
        InterestOnBalancePolicy interestOnBalancePolicy = GetInterestOnBalancePolicy();

        _context.Writer.Write("Enter deposit term: ");
        var term = TimeSpan.Parse(_context.Reader.ReadLine());
        _context.Writer.Write("Enter interest calculation period: ");
        var period = TimeSpan.Parse(_context.Reader.ReadLine());

        return bank.AccountTypeManager.CreateDepositAccountType(term, interestOnBalancePolicy, period);
    }

    private InterestOnBalancePolicy GetInterestOnBalancePolicy()
    {
        var interestOnBalancePolicyLayers = new List<InterestOnBalanceLayer>();
        _context.Writer.Write("Enter layer: ");
        string input;
        while ((input = _context.Reader.ReadLine()) != string.Empty)
        {
            string[] values = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var requiredBalance = values[0].ToMoneyAmount();
            decimal interest = decimal.Parse(values[1]);
            var layer = new InterestOnBalanceLayer(requiredBalance, interest);
            interestOnBalancePolicyLayers.Add(layer);
            _context.Writer.Write("Enter layer: ");
        }

        return new InterestOnBalancePolicy(interestOnBalancePolicyLayers);
    }
}