using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.CustomerHandlers;

public class CustomerInformationPassportDataSetterHandler : Handler
{
    private readonly AppContext _context;

    public CustomerInformationPassportDataSetterHandler(AppContext context)
        : base("passport")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var customerId = args[1].ToGuid();
        var passportData = new PassportData(DateOnly.Parse(args[3]), args[2]);

        ICustomer customer = _context.CentralBank.Banks.SelectMany(bank => bank.Customers).Distinct()
            .Single(customer => customer.Id.Equals(customerId));
        customer.SetPassportData(passportData);
    }
}