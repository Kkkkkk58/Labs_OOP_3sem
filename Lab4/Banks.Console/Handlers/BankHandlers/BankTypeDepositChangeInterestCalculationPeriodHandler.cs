using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDepositChangeInterestCalculationPeriodHandler : Handler
{
    private readonly AppContext _context;

    public BankTypeDepositChangeInterestCalculationPeriodHandler(AppContext context)
        : base("period")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        INoTransactionalBank bank = GetBank();
        Guid typeId = GetTypeId();
        TimeSpan interestCalculationPeriod = GetInterestCalculationPeriod();
        bank.AccountTypeManager.ChangeInterestCalculationPeriod(typeId, interestCalculationPeriod);
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

    private Guid GetTypeId()
    {
        _context.Writer.Write("Enter type id: ");
        return _context.Reader.ReadLine().ToGuid();
    }

    private TimeSpan GetInterestCalculationPeriod()
    {
        _context.Writer.Write("Enter interest calculation period: ");
        return TimeSpan.Parse(_context.Reader.ReadLine());
    }
}