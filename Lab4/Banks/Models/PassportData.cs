using Banks.Models.Abstractions;

namespace Banks.Models;

public record PassportData : IDocumentData
{
    public PassportData(DateOnly dateOfIssue, string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentNullException(nameof(number));

        DateOfIssue = dateOfIssue;
        Number = number;
    }

    public DateOnly DateOfIssue { get; }
    public string Number { get; }
}