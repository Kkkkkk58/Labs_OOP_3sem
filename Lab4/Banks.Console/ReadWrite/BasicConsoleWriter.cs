using Banks.Console.ReadWrite.Abstractions;

namespace Banks.Console.ReadWrite;

public class BasicConsoleWriter : IWriter
{
    public void Write(object value)
    {
        System.Console.Write(value);
    }

    public void WriteLine(object value)
    {
        System.Console.WriteLine(value);
        System.Console.WriteLine();
    }
}