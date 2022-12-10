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
        ICustomer customer = GetCustomer();
        PassportData passportData = GetPassportData();

        customer.SetPassportData(passportData);

        _context.Writer.WriteLine($"Set new passport data for customer {customer.Id}");
    }

    private ICustomer GetCustomer()
    {
        _context.Writer.Write("Enter customer id: ");
        var customerId = _context.Reader.ReadLine().ToGuid();

        return _context
            .CentralBank
            .Banks
            .SelectMany(bank => bank.Customers)
            .Distinct()
            .Single(customer => customer.Id.Equals(customerId));
    }

    private PassportData GetPassportData()
    {
        _context.Writer.Write("Enter passport of number: ");
        string number = _context.Reader.ReadLine();
        _context.Writer.Write("Enter date of issue: ");
        var dateOfIssue = DateOnly.Parse(_context.Reader.ReadLine());

        return new PassportData(dateOfIssue, number);
    }
}