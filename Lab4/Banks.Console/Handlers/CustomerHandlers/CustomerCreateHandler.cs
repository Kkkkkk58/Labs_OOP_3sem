using Banks.Builders.Abstractions;
using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Console.Notifier;
using Banks.Entities;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.CustomerHandlers;

public class CustomerCreateHandler : Handler
{
    private readonly AppContext _context;

    public CustomerCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context.CentralBank.Banks.Single(bank => bank.Id.Equals(bankId));

        ICustomerBuilder customerBuilder = Customer.Builder;
        _context.Writer.Write("Enter first name: ");
        ICustomerLastNameBuilder lastNameBuilder =
            customerBuilder.SetFirstName(_context.Reader.ReadLine() ?? throw new NotImplementedException());
        _context.Writer.Write("Enter last name: ");
        IOptionalCustomerInformationBuilder optionalInfoBuilder =
            lastNameBuilder.SetLastName(_context.Reader.ReadLine() ?? throw new NotImplementedException());
        optionalInfoBuilder.SetNotifier(new ConsoleNotifier());
        _context.Writer.Write("Enter passport data [optional]: ");
        string[]? input = _context.Reader.ReadLine()?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (input is not null && input.Length == 2)
        {
            var passportData = new PassportData(DateOnly.Parse(input[1]), input[0]);
            optionalInfoBuilder.SetPassportData(passportData);
        }

        _context.Writer.Write("Enter address [optional]: ");
        input = _context.Reader.ReadLine()?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (input is not null && input.Length == 1)
        {
            var address = new Address(input[0]);
            optionalInfoBuilder.SetAddress(address);
        }

        ICustomer customer = optionalInfoBuilder.Build();
        bank.RegisterCustomer(customer);
        _context.Writer.WriteLine($"Successfully created customer {customer.Id}");
    }
}