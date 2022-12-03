namespace Banks.Console.ReadWrite.Abstractions;

public interface IWriter
{
    void Write(object value);
    void WriteLine(object value);
}