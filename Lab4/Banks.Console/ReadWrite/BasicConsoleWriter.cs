using Banks.Console.ReadWrite.Abstractions;

namespace Banks.Console.ReadWrite;

public class BasicConsoleWriter : IWriter
{
    public void Write(object value)
    {
        System.Console.WriteLine(value);
    }

    public void WriteLine(object value)
    {
        System.Console.WriteLine(value);
    }
}