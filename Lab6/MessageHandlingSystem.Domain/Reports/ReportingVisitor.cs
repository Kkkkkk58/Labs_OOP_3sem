using MessageHandlingSystem.Domain.Employees;
using MessageHandlingSystem.Domain.MessageSources;

namespace MessageHandlingSystem.Domain.Reports;

public class ReportingVisitor : IReportingVisitor
{
    private readonly Guid _id;
    private readonly DateTime _from;
    private readonly DateTime _to;
    private readonly Guid _authorId;
    private readonly Dictionary<Guid, int> _messagesHandledByEmployee;
    private readonly Dictionary<Guid, int> _messagesReceivedBySource;

    public ReportingVisitor(Guid id, DateTime from, DateTime to, Guid authorId)
    {
        _id = id;
        _from = from;
        _to = to;
        _authorId = authorId;
        _messagesHandledByEmployee = new Dictionary<Guid, int>();
        _messagesReceivedBySource = new Dictionary<Guid, int>();
    }

    public void Visit(Manager manager)
    {
        foreach (Employee managerSubordinate in manager.Subordinates)
        {
            managerSubordinate.Accept(this);
        }

        GetEmployeeStats(manager);
    }

    public void Visit(Subordinate subordinate)
    {
        GetEmployeeStats(subordinate);
    }

    public Report GetReport()
    {
        return new Report(_id, _authorId, _from, _to, _messagesHandledByEmployee, _messagesReceivedBySource);
    }

    private void GetEmployeeStats(Employee employee)
    {
        _messagesHandledByEmployee.Add(
            employee.Id,
            employee.HandledMessages.Count(msg => msg.HandlingTime <= _to && msg.HandlingTime >= _from));

        foreach (MessageSource src in employee.Accounts.SelectMany(account => account.MessageSources).Distinct())
        {
            _messagesReceivedBySource.Add(
                src.Id,
                src.ReceivedMessages.Count(x => x.SendingTime <= _to && x.SendingTime >= _from));
        }
    }
}