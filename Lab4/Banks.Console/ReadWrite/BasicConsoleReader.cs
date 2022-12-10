using Banks.Console.ReadWrite.Abstractions;

namespace Banks.Console.ReadWrite;

public class BasicConsoleReader : IReader
{
    public string ReadLine()
    {
        return System.Console.ReadLine() ?? throw new NotImplementedException();
    }
}