using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Domain.Accounts;
using MessageHandlingSystem.Domain.Employees;
using MessageHandlingSystem.Domain.Messages;
using MessageHandlingSystem.Domain.Messages.MessageStates;
using MessageHandlingSystem.Domain.MessageSources;
using MessageHandlingSystem.Domain.Reports;
using MessageHandlingSystem.Infrastructure.DataAccess.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace MessageHandlingSystem.Infrastructure.DataAccess;

public sealed partial class DatabaseContext : DbContext, IDataBaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        // TODO Migrate on Startup
        Database.EnsureCreated();
        SaveChangesFailed += (sender, args) =>
            throw new DbUpdateException($"Failed to save changes: {args.Exception.Message}", args.Exception);
    }

    public DbSet<Employee> Employees { get; private init; } = null!;
    public DbSet<Manager> Managers { get; private init; } = null!;
    public DbSet<Subordinate> Subordinates { get; private init; } = null!;
    public DbSet<Message> Messages { get; private init; } = null!;
    public DbSet<MessageSource> MessageSources { get; private init; } = null!;
    public DbSet<Account> Accounts { get; private init; } = null!;
    public DbSet<Report> Reports { get; private init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<IMessageState>().HaveConversion<MessageStateConverter>();
        configurationBuilder.Properties<IReadOnlyDictionary<Guid, int>>()
            .HaveConversion<DictionaryConverter<Guid, int>>();
    }
}