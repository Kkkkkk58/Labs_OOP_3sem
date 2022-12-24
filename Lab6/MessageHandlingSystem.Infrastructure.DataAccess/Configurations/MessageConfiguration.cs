using MessageHandlingSystem.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessageHandlingSystem.Infrastructure.DataAccess.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasDiscriminator<string>("Discriminator").HasValue<EmailMessage>(nameof(EmailMessage))
            .HasValue<PhoneMessage>(nameof(PhoneMessage)).HasValue<MessengerMessage>(nameof(MessengerMessage));
    }
}