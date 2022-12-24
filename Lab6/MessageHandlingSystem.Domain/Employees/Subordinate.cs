using MessageHandlingSystem.Domain.Reports;

namespace MessageHandlingSystem.Domain.Employees;

public partial class Subordinate : Employee
{
    public Subordinate(Guid id, string name)
        : base(id, name)
    {
    }

    public override void Accept(IReportingVisitor reportingVisitor)
    {
        reportingVisitor.Visit(this);
    }
}