using MessageHandlingSystem.Application.Dto.Accounts;
using MessageHandlingSystem.Application.Mapping.Messages;
using MessageHandlingSystem.Application.Mapping.MessageSources;
using MessageHandlingSystem.Domain.Accounts;

namespace MessageHandlingSystem.Application.Mapping.Accounts;

public static class AccountMapping
{
    public static AccountDto AsDto(this Account account)
    {
        return new AccountDto(account.Id, account.MessageSources.Select(x => x.AsDto()).ToArray(), account.LoadedMessages.Select(x => x.AsDto()).ToArray());
    }
}