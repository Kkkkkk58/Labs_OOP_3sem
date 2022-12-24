using MessageHandlingSystem.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessageHandlingSystem.Infrastructure.DataAccess.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Navigation(x => x.Accounts).HasField("_accounts");
        builder.Navigation(x => x.HandledMessages).HasField("_handledMessages");
        builder.HasDiscriminator<string>("Discriminator").HasValue<Manager>(nameof(Manager))
            .HasValue<Subordinate>(nameof(Subordinate));
    }
}