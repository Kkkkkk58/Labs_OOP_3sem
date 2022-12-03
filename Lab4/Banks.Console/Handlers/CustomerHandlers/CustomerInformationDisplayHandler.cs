using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Console.ViewModels;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.CustomerHandlers;

public class CustomerInformationDisplayHandler : Handler
{
    private readonly AppContext _context;

    public CustomerInformationDisplayHandler(AppContext context)
        : base("show")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var customerId = args[1].ToGuid();
        ICustomer customer = _context
            .CentralBank
            .Banks
            .SelectMany(bank => bank.Customers)
            .Distinct()
            .Single(c => c.Id.Equals(customerId));

        _context.Writer.WriteLine(new CustomerViewModel(customer));
    }
}