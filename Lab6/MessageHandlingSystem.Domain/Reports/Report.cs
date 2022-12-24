using RichEntity.Annotations;

namespace MessageHandlingSystem.Domain.Reports;

public partial class Report : IEntity<Guid>
{
    public Report(Guid id, Guid authorId, DateTime from, DateTime to, IReadOnlyDictionary<Guid, int> messagesHandledByEmployee, IReadOnlyDictionary<Guid, int> messagesReceivedBySource)
    {
        Id = id;
        AuthorId = authorId;
        From = from;
        To = to;
        MessagesHandledByEmployee = messagesHandledByEmployee;
        MessagesReceivedBySource = messagesReceivedBySource;
    }

    public Guid AuthorId { get; init; }
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public IReadOnlyDictionary<Guid, int> MessagesHandledByEmployee { get; init; }
    public IReadOnlyDictionary<Guid, int> MessagesReceivedBySource { get; init; }
    public int TotalMessages => MessagesReceivedBySource.Select(d => d.Value).Sum();
}