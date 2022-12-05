namespace Banks.Models;

public record Message
{
    public Message(string sender, string topic, string content, DateTime messageDate)
    {
        if (string.IsNullOrWhiteSpace(sender))
            throw new ArgumentNullException(nameof(sender));
        if (string.IsNullOrWhiteSpace(topic))
            throw new ArgumentNullException(nameof(topic));
        if (string.IsNullOrWhiteSpace(sender))
            throw new ArgumentNullException(nameof(content));

        Sender = sender;
        Topic = topic;
        Content = content;
        MessageDate = messageDate;
    }

    public string Sender { get; }
    public string Topic { get; }
    public string Content { get; }
    public DateTime MessageDate { get; }
}