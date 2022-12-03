using Banks.Builders.Abstractions;
using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Console.Notifier;
using Banks.Entities;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Controllers.CustomerHandlers;

public class CustomerCreateHandler : Handler
{
    private readonly AppContext _context;

    public CustomerCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context.CentralBank.Banks.Single(bank => bank.Id.Equals(bankId));

        ICustomerBuilder customerBuilder = Customer.Builder;
        System.Console.Write("Enter first name: ");
        ICustomerLastNameBuilder lastNameBuilder = customerBuilder.SetFirstName(System.Console.ReadLine() ?? throw new NotImplementedException());
        System.Console.Write("Enter last name: ");
        IOptionalCustomerInformationBuilder optionalInfoBuilder =
            lastNameBuilder.SetLastName(System.Console.ReadLine() ?? throw new NotImplementedException());
        optionalInfoBuilder.SetNotifier(new ConsoleNotifier());
        System.Console.Write("Enter passport data [optional]: ");
        string[]? input = System.Console.ReadLine()?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (input is not null && input.Length == 2)
        {
            var passportData = new PassportData(DateOnly.Parse(input[1]), input[0]);
            optionalInfoBuilder.SetPassportData(passportData);
        }

        System.Console.Write("Enter address [optional]: ");
        input = System.Console.ReadLine()?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (input is not null && input.Length == 1)
        {
            var address = new Address(input[0]);
            optionalInfoBuilder.SetAddress(address);
        }

        ICustomer customer = optionalInfoBuilder.Build();
        bank.RegisterCustomer(customer);
        System.Console.WriteLine(customer.Id);
        base.Handle(args);
    }
}