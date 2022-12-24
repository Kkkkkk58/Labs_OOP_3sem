using MessageHandlingSystem.Domain.MessageSources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessageHandlingSystem.Infrastructure.DataAccess.Configurations;

public class MessageSourceConfiguration : IEntityTypeConfiguration<MessageSource>
{
    public void Configure(EntityTypeBuilder<MessageSource> builder)
    {
        builder.Navigation(x => x.ReceivedMessages).HasField("_receivedMessages");

        builder.HasDiscriminator<string>("Discriminator").HasValue<EmailMessageSource>(nameof(EmailMessageSource))
            .HasValue<PhoneMessageSource>(nameof(PhoneMessageSource)).HasValue<MessengerMessageSource>(nameof(MessengerMessageSource));
    }
}