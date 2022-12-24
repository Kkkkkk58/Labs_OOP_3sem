using MessageHandlingSystem.Domain.Employees;

namespace MessageHandlingSystem.Domain.Reports;

public interface IReportingVisitor
{
    void Visit(Manager manager);
    void Visit(Subordinate subordinate);
    Report GetReport();
}