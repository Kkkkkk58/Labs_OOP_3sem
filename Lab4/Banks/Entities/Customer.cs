﻿using Banks.Builders.Abstractions;
using Banks.Entities.Abstractions;
using Banks.EventArgs;
using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class Customer : ICustomer, IEquatable<Customer>
{
    private Address? _address;
    private PassportData? _passportData;
    private ICustomerNotifier? _notifier;

    private Customer(
        Guid id,
        string firstName,
        string lastName,
        ICustomerNotifier? notifier,
        Address? address,
        PassportData? passportData)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        _address = address;
        _passportData = passportData;
        _notifier = notifier;
    }

    public static ICustomerBuilder Builder => new CustomerBuilder();
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public bool IsVerified => _address is not null && _passportData is not null;

    public Address SetAddress(Address address)
    {
        ArgumentNullException.ThrowIfNull(address);
        _address = address;

        return _address;
    }

    public PassportData SetPassportData(PassportData passportData)
    {
        ArgumentNullException.ThrowIfNull(passportData);
        _passportData = passportData;

        return _passportData;
    }

    public ICustomerNotifier SetNotifier(ICustomerNotifier notifier)
    {
        ArgumentNullException.ThrowIfNull(notifier);

        _notifier = notifier;
        return _notifier;
    }

    public ICustomerNotifier AddNotifier(ICustomerNotifierDecoratorBuilder decoratorBuilder)
    {
        ArgumentNullException.ThrowIfNull(decoratorBuilder);

        if (_notifier is null)
            throw CustomerException.NotifierIsNotSet(Id);

        _notifier = decoratorBuilder
            .SetWrapped(_notifier)
            .Build();
        return _notifier;
    }

    public void Update(object? sender, CustomerAccountChangesEventArgs eventArgs)
    {
        ArgumentNullException.ThrowIfNull(eventArgs);

        _notifier?.Send(eventArgs.Message);
    }

    public bool Equals(Customer? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Customer);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    private class CustomerBuilder : ICustomerBuilder, ICustomerLastNameBuilder, IOptionalCustomerInformationBuilder
    {
        private string? _firstName;
        private string? _lastName;
        private ICustomerNotifier? _notifier;
        private Address? _address;
        private PassportData? _passportData;

        public ICustomerLastNameBuilder SetFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public IOptionalCustomerInformationBuilder SetLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public IOptionalCustomerInformationBuilder SetAddress(Address address)
        {
            _address = address;
            return this;
        }

        public IOptionalCustomerInformationBuilder SetPassportData(PassportData passportData)
        {
            _passportData = passportData;
            return this;
        }

        public IOptionalCustomerInformationBuilder SetNotifier(ICustomerNotifier notifier)
        {
            _notifier = notifier;
            return this;
        }

        public ICustomer Build()
        {
            if (string.IsNullOrWhiteSpace(_firstName))
                throw new ArgumentNullException(nameof(_firstName));
            if (string.IsNullOrWhiteSpace(_lastName))
                throw new ArgumentNullException(nameof(_lastName));

            ICustomer customer =
                new Customer(Guid.NewGuid(), _firstName, _lastName, _notifier, _address, _passportData);
            Reset();

            return customer;
        }

        private void Reset()
        {
            _firstName = null;
            _lastName = null;
            _address = null;
            _passportData = null;
        }
    }
}