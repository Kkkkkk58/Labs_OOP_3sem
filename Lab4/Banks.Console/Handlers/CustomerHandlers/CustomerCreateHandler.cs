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
        INoTransactionalBank bank = GetBank();
        ICustomer customer = GetCustomer();
        bank.RegisterCustomer(customer);
        _context.Writer.WriteLine($"Successfully created customer {customer.Id}");
    }

    private INoTransactionalBank GetBank()
    {
        _context.Writer.Write("Enter bank id: ");
        var bankId = _context.Reader.ReadLine().ToGuid();

        return _context
            .CentralBank
            .Banks
            .Single(bank => bank.Id.Equals(bankId));
    }

    private ICustomer GetCustomer()
    {
        ICustomerBuilder customerBuilder = Customer.Builder;
        _context.Writer.Write("Enter first name: ");
        ICustomerLastNameBuilder lastNameBuilder = customerBuilder
            .SetFirstName(_context.Reader.ReadLine());
        _context.Writer.Write("Enter last name: ");
        IOptionalCustomerInformationBuilder optionalInfoBuilder = lastNameBuilder
            .SetLastName(_context.Reader.ReadLine());
        SetCustomerOptionalData(optionalInfoBuilder);

        return optionalInfoBuilder.Build();
    }

    private void SetCustomerOptionalData(IOptionalCustomerInformationBuilder optionalInfoBuilder)
    {
        optionalInfoBuilder.SetNotifier(new ConsoleNotifier());
        SetPassportData(optionalInfoBuilder);
        SetAddress(optionalInfoBuilder);
    }

    private void SetPassportData(IOptionalCustomerInformationBuilder optionalInfoBuilder)
    {
        _context.Writer.Write("Enter passport data [optional]: ");
        string[] input = _context.Reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (input.Length != 2)
            return;

        var passportData = new PassportData(DateOnly.Parse(input[1]), input[0]);
        optionalInfoBuilder.SetPassportData(passportData);
    }

    private void SetAddress(IOptionalCustomerInformationBuilder optionalInfoBuilder)
    {
        _context.Writer.Write("Enter address [optional]: ");
        string[] input = _context.Reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (input.Length != 1)
            return;

        var address = new Address(input[0]);
        optionalInfoBuilder.SetAddress(address);
    }
}