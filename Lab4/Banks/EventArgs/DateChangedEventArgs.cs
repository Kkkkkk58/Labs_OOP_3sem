namespace Banks.EventArgs;

public class DateChangedEventArgs : System.EventArgs
{
    public DateChangedEventArgs(DateTime date)
    {
        Date = date;
    }

    public DateTime Date { get; }
}