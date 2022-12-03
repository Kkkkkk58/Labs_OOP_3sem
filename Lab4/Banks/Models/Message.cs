namespace Banks.Models;

public record Message
{
    public Message(string sender, string topic, string content, DateTime messageDate)
    {
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