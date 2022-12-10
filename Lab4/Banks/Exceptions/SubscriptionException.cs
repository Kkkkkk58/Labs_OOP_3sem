namespace Banks.Exceptions;

public class SubscriptionException : BanksException
{
    private SubscriptionException(string message)
        : base(message)
    {
    }

    public static SubscriptionException AlreadySubscribed(Guid subscriberId)
    {
        return new SubscriptionException($"Subscriber {subscriberId} is already subscribed to notifications");
    }

    public static SubscriptionException SubscriberNotFound(Guid subscriberId)
    {
        return new SubscriptionException($"Subscriber {subscriberId} was not found");
    }
}