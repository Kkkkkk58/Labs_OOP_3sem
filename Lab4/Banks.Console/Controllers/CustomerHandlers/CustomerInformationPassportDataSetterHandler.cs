using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Controllers.CustomerHandlers;

public class CustomerInformationPassportDataSetterHandler : Handler
{
    private readonly AppContext _context;

    public CustomerInformationPassportDataSetterHandler(AppContext context)
        : base("passport")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var customerId = args[1].ToGuid();
        var passportData = new PassportData(DateOnly.Parse(args[3]), args[2]);

        ICustomer customer = _context.CentralBank.Banks.SelectMany(bank => bank.Customers).Distinct()
            .Single(customer => customer.Id.Equals(customerId));
        customer.SetPassportData(passportData);

        base.Handle(args);
    }
}