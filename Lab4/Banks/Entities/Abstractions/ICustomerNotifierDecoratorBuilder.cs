namespace Banks.Entities.Abstractions;

public interface ICustomerNotifierDecoratorBuilder
{
    ICustomerNotifierBuilder SetWrapped(ICustomerNotifier wrapped);
}

public interface ICustomerNotifierBuilder
{
    ICustomerNotifier Build();
}