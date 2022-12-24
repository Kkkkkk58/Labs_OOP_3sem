using MessageHandlingSystem.Domain.Reports;

namespace MessageHandlingSystem.Domain.Employees;

public partial class Manager : Employee
{
    private readonly List<Employee> _subordinates;

    public Manager(Guid id, string name)
        : base(id, name)
    {
        _subordinates = new List<Employee>();
    }

    public virtual IReadOnlyCollection<Employee> Subordinates => _subordinates;

    public Report MakeReport(Guid id, DateTime from, DateTime to)
    {
        var reportingVisitor = new ReportingVisitor(id, from, to, Id);
        Accept(reportingVisitor);
        return reportingVisitor.GetReport();
    }

    public Employee AddSubordinate(Employee employee)
    {
        if (_subordinates.Contains(employee))
            throw new NotImplementedException();

        _subordinates.Add(employee);
        return employee;
    }

    public void RemoveSubordinate(Employee employee)
    {
        if (!_subordinates.Remove(employee))
            throw new NotImplementedException();
    }

    public override void Accept(IReportingVisitor reportingVisitor)
    {
        reportingVisitor.Visit(this);
    }
}