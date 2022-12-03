using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Controllers.CustomerHandlers;

public class CustomerInformationDisplayHandler : Handler
{
    private readonly AppContext _context;

    public CustomerInformationDisplayHandler(AppContext context)
        : base("show")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var customerId = args[1].ToGuid();
        ICustomer customer = _context.CentralBank.Banks.SelectMany(bank => bank.Customers).Distinct()
            .Single(c => c.Id.Equals(customerId));

        System.Console.WriteLine(customer);
        base.Handle(args);
    }
}