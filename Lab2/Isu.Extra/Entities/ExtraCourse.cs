using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class ExtraCourse : IEquatable<ExtraCourse>
{
    private readonly List<ExtraStream> _streams;

    public ExtraCourse(MegaFaculty provider, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw ExtraCourseException.EmptyName();

        Id = Guid.NewGuid();
        Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        Name = name;
        _streams = new List<ExtraStream>();
    }

    public Guid Id { get; }
    public MegaFaculty Provider { get; }
    public string Name { get; }
    public IReadOnlyList<ExtraStream> Streams => _streams.AsReadOnly();

    public ExtraStream AddStream(ExtraStream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        if (_streams.Contains(stream))
            throw ExtraCourseException.StreamAlreadyExists(Id, stream.Id);
        if (!Equals(stream.Course))
            throw ExtraStreamException.StreamBelongsToOtherExtraCourse(Id, stream.Id, stream.Course.Id);

        _streams.Add(stream);
        return stream;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ExtraCourse);
    }

    public bool Equals(ExtraCourse? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}