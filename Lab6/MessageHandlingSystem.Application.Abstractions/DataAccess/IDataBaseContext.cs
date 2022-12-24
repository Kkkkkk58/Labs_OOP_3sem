using MessageHandlingSystem.Domain.Accounts;
using MessageHandlingSystem.Domain.Employees;
using MessageHandlingSystem.Domain.Messages;
using MessageHandlingSystem.Domain.MessageSources;
using MessageHandlingSystem.Domain.Reports;
using Microsoft.EntityFrameworkCore;

namespace MessageHandlingSystem.Application.Abstractions.DataAccess;

public interface IDataBaseContext
{
    DbSet<Employee> Employees { get; }
    DbSet<Manager> Managers { get; }
    DbSet<Subordinate> Subordinates { get; }
    DbSet<Message> Messages { get; }
    DbSet<MessageSource> MessageSources { get; }
    DbSet<Account> Accounts { get; }
    DbSet<Report> Reports { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}