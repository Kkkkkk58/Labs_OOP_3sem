using Banks.Console.Chains;
using Banks.Console.Controllers;
using Banks.Console.Controllers.AccountHandlers;
using Banks.Console.Controllers.BankHandlers;
using Banks.Console.Controllers.CustomerHandlers;
using Banks.Console.Controllers.OperationsHandlers;
using Banks.Console.Controllers.TimeHandlers;
using Banks.Tools.Abstractions;

namespace Banks.Console;

public class CommandTreeConfigurator
{
    private readonly AppContext _context;
    private readonly IFastForwardingClock _clock;

    public CommandTreeConfigurator(AppContext context, IFastForwardingClock clock)
    {
        _context = context;
        _clock = clock;
    }

    public IHandler Configure()
    {
        var mainHandler = new MainHandler();
        TimeController timeHandler = ConfigureTimeHandler();
        OperationController operationHandler = ConfigureOperationHandler();
        CustomerController customerHandler = ConfigureCustomerHandler();
        BankController bankHandler = ConfigureBankHandler();
        AccountController accountHandler = ConfigureAccountHandler();

        return mainHandler
            .AddSubHandler(timeHandler)
            .AddSubHandler(operationHandler)
            .AddSubHandler(customerHandler)
            .AddSubHandler(bankHandler)
            .AddSubHandler(accountHandler);
    }

    private AccountController ConfigureAccountHandler()
    {
        var accountHandler = new AccountController();
        var accountDisplayHandler = new AccountDisplayHandler(_context);
        AccountCreateHandler accountCreateHandler = ConfigureAccountCreateHandler();

        accountHandler
            .AddSubHandler(accountDisplayHandler)
            .AddSubHandler(accountCreateHandler);
        return accountHandler;
    }

    private AccountCreateHandler ConfigureAccountCreateHandler()
    {
        var accountCreateHandler = new AccountCreateHandler();
        accountCreateHandler
            .AddSubHandler(new AccountCreateCreditHandler(_context))
            .AddSubHandler(new AccountCreateDebitHandler(_context))
            .AddSubHandler(new AccountCreateDepositHandler(_context));

        return accountCreateHandler;
    }

    private BankController ConfigureBankHandler()
    {
        var bankHandler = new BankController();
        BankChangeHandler bankChangeHandler = ConfigureBankChangeHandler();
        BankCreateHandler bankCreateHandler = ConfigureBankCreateHandler();
        BankTypeHandler bankTypeHandler = ConfigureBankTypeHandler();

        bankHandler
            .AddSubHandler(bankTypeHandler)
            .AddSubHandler(bankChangeHandler)
            .AddSubHandler(bankCreateHandler);

        return bankHandler;
    }

    private BankChangeHandler ConfigureBankChangeHandler()
    {
        var bankChangeHandler = new BankChangeHandler();
        bankChangeHandler
            .AddSubHandler(new BankChangeSuspiciousOperationsLimitHandler(_context));

        return bankChangeHandler;
    }

    private BankCreateHandler ConfigureBankCreateHandler()
    {
        return new BankCreateHandler(_context);
    }

    private BankTypeHandler ConfigureBankTypeHandler()
    {
        var bankTypeHandler = new BankTypeHandler();
        BankTypeDepositHandler bankTypeDepositHandler = ConfigureBankTypeDepositHandler();
        BankTypeDebitHandler bankTypeDebitHandler = ConfigureBankTypeDebitHandler();
        BankTypeCreditHandler bankTypeCreditHandler = ConfigureBankTypeCreditHandler();

        bankTypeHandler
            .AddSubHandler(bankTypeDepositHandler)
            .AddSubHandler(bankTypeDebitHandler)
            .AddSubHandler(bankTypeCreditHandler);

        return bankTypeHandler;
    }

    private BankTypeDepositHandler ConfigureBankTypeDepositHandler()
    {
        var bankTypeDepositHandler = new BankTypeDepositHandler();
        BankTypeDepositCreateHandler bankTypeDepositCreateHandler = ConfigureBankTypeDepositCreateHandler();
        BankTypeDepositChangeHandler bankTypeDepositChangeHandler = ConfigureBankTypeDepositChangeHandler();

        bankTypeDepositHandler
            .AddSubHandler(bankTypeDepositCreateHandler)
            .AddSubHandler(bankTypeDepositChangeHandler);
        return bankTypeDepositHandler;
    }

    private BankTypeDepositChangeHandler ConfigureBankTypeDepositChangeHandler()
    {
        var bankTypeDepositChangeHandler = new BankTypeDepositChangeHandler();
        bankTypeDepositChangeHandler
            .AddSubHandler(new BankTypeDepositChangeInterestCalculationPeriodHandler(_context))
            .AddSubHandler(new BankTypeDepositChangeTermHandler(_context));

        return bankTypeDepositChangeHandler;
    }

    private BankTypeDepositCreateHandler ConfigureBankTypeDepositCreateHandler()
    {
        return new BankTypeDepositCreateHandler(_context);
    }

    private BankTypeDebitHandler ConfigureBankTypeDebitHandler()
    {
        var bankTypeDebitHandler = new BankTypeDebitHandler();
        BankTypeDebitCreateHandler bankTypeDebitCreateHandler = ConfigureBankTypeDebitCreateHandler();
        BankTypeDebitChangeHandler bankTypeDebitChangeHandler = ConfigureBankTypeDebitChangeHandler();

        bankTypeDebitHandler
            .AddSubHandler(bankTypeDebitCreateHandler)
            .AddSubHandler(bankTypeDebitChangeHandler);
        return bankTypeDebitHandler;
    }

    private BankTypeDebitCreateHandler ConfigureBankTypeDebitCreateHandler()
    {
        return new BankTypeDebitCreateHandler(_context);
    }

    private BankTypeDebitChangeHandler ConfigureBankTypeDebitChangeHandler()
    {
        var bankTypeDebitChangeHandler = new BankTypeDebitChangeHandler();
        bankTypeDebitChangeHandler
            .AddSubHandler(new BankTypeDebitChangeInterestHandler(_context))
            .AddSubHandler(new BankTypeDebitChangeInterestCalculationPeriodHandler(_context));

        return bankTypeDebitChangeHandler;
    }

    private BankTypeCreditHandler ConfigureBankTypeCreditHandler()
    {
        var bankTypeCreditHandler = new BankTypeCreditHandler();
        BankTypeCreditCreateHandler bankTypeCreditCreateHandler = ConfigureBankTypeCreditCreateHandler();
        BankTypeCreditChangeHandler bankTypeCreditChangeHandler = ConfigureBankTypeCreditChangeHandler();

        bankTypeCreditHandler
            .AddSubHandler(bankTypeCreditCreateHandler)
            .AddSubHandler(bankTypeCreditChangeHandler);
        return bankTypeCreditHandler;
    }

    private BankTypeCreditCreateHandler ConfigureBankTypeCreditCreateHandler()
    {
        return new BankTypeCreditCreateHandler(_context);
    }

    private BankTypeCreditChangeHandler ConfigureBankTypeCreditChangeHandler()
    {
        var bankTypeCreditChangeHandler = new BankTypeCreditChangeHandler();
        bankTypeCreditChangeHandler
            .AddSubHandler(new BankTypeCreditChangeChargeHandler(_context))
            .AddSubHandler(new BankTypeCreditChangeDebtLimitHandler(_context));
        return bankTypeCreditChangeHandler;
    }

    private CustomerController ConfigureCustomerHandler()
    {
        var customerHandler = new CustomerController();
        CustomerInformationHandler customerInformationHandler = ConfigureCustomerInformationHandler();
        CustomerCreateHandler customerCreateHandler = ConfigureCustomerCreateHandler();

        customerHandler
            .AddSubHandler(customerInformationHandler)
            .AddSubHandler(customerCreateHandler);

        return customerHandler;
    }

    private CustomerInformationHandler ConfigureCustomerInformationHandler()
    {
        var customerInformationHandler = new CustomerInformationHandler();
        CustomerInformationDisplayHandler displayHandler = ConfigureCustomerInformationDisplayHandler();
        CustomerInformationSetterHandler setterHandler = ConfigureCustomerInformationSetterHandler();
        CustomerInformationAccountsHandler accountsHandler = ConfigureCustomerInformationAccountsHandler();

        customerInformationHandler
            .AddSubHandler(displayHandler)
            .AddSubHandler(setterHandler)
            .AddSubHandler(accountsHandler);
        return customerInformationHandler;
    }

    private CustomerInformationSetterHandler ConfigureCustomerInformationSetterHandler()
    {
        var customerInformationSetterHandler = new CustomerInformationSetterHandler();
        customerInformationSetterHandler
            .AddSubHandler(new CustomerInformationAddressSetterHandler(_context))
            .AddSubHandler(new CustomerInformationPassportDataSetterHandler(_context));

        return customerInformationSetterHandler;
    }

    private CustomerInformationAccountsHandler ConfigureCustomerInformationAccountsHandler()
    {
        return new CustomerInformationAccountsHandler(_context);
    }

    private CustomerInformationDisplayHandler ConfigureCustomerInformationDisplayHandler()
    {
        return new CustomerInformationDisplayHandler(_context);
    }

    private CustomerCreateHandler ConfigureCustomerCreateHandler()
    {
        return new CustomerCreateHandler(_context);
    }

    private OperationController ConfigureOperationHandler()
    {
        var operationHandler = new OperationController();
        OperationHistoryHandler operationHistoryHandler = ConfigureOperationHistoryHandler();

        operationHandler
            .AddSubHandler(new OperationCancellationHandler(_context))
            .AddSubHandler(new OperationShowHandler(_context))
            .AddSubHandler(new ReplenishmentOperationHandler(_context))
            .AddSubHandler(new TransferOperationHandler(_context))
            .AddSubHandler(new WithdrawalOperationHandler(_context))
            .AddSubHandler(operationHistoryHandler);

        return operationHandler;
    }

    private OperationHistoryHandler ConfigureOperationHistoryHandler()
    {
        var operationHistoryHandler = new OperationHistoryHandler();
        operationHistoryHandler
            .AddSubHandler(new AccountOperationHistoryHandler(_context))
            .AddSubHandler(new OverallOperationHistoryHandler(_context));

        return operationHistoryHandler;
    }

    private TimeController ConfigureTimeHandler()
    {
        var timeHandler = new TimeController();
        TimeSkipHandler timeSkipHandler = ConfigureTimeSkipHandler();

        timeHandler.AddSubHandler(timeSkipHandler);
        return timeHandler;
    }

    private TimeSkipHandler ConfigureTimeSkipHandler()
    {
        var timeSkipHandler = new TimeSkipHandler();
        timeSkipHandler
            .AddSubHandler(new TimeSkipDaysHandler(_clock))
            .AddSubHandler(new TimeSkipMonthsHandler(_clock))
            .AddSubHandler(new TimeSkipTimeSpanHandler(_clock))
            .AddSubHandler(new TimeSkipYearsHandler(_clock));
        return timeSkipHandler;
    }
}