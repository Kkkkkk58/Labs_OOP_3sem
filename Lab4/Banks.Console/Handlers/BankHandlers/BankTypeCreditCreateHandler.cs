using Banks.AccountTypes.Abstractions;
using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeCreditCreateHandler : Handler
{
    private readonly AppContext _context;

    public BankTypeCreditCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        INoTransactionalBank bank = GetBank();
        ICreditAccountType type = GetType(bank);

        _context.Writer.WriteLine($"Successfully created credit type {type.Id}");
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

    private ICreditAccountType GetType(INoTransactionalBank bank)
    {
        _context.Writer.Write("Enter debt limit: ");
        var debtLimit = _context.Reader.ReadLine().ToMoneyAmount();
        _context.Writer.Write("Enter charge: ");
        var charge = _context.Reader.ReadLine().ToMoneyAmount();
        return bank.AccountTypeManager.CreateCreditAccountType(debtLimit, charge);
    }
}