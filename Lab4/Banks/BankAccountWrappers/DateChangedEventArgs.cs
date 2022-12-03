namespace Banks.BankAccountWrappers;

public class DateChangedEventArgs : EventArgs
{
    public DateChangedEventArgs(DateTime date)
    {
        Date = date;
    }

    public DateTime Date { get; }
}