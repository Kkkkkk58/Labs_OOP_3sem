using MessageHandlingSystem.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessageHandlingSystem.Infrastructure.DataAccess.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Navigation(x => x.MessageSources).HasField("_messageSources");
        builder.Navigation(x => x.LoadedMessages).HasField("_loadedMessages");
    }
}