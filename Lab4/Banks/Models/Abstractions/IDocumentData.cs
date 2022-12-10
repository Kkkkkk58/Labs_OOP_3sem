namespace Banks.Models.Abstractions;

public interface IDocumentData
{
    DateOnly DateOfIssue { get; }
    string Number { get; }
}