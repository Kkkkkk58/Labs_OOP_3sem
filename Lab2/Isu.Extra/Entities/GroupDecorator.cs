using Isu.Entities;
using Isu.Models.IsuInformationDetails;

namespace Isu.Extra.Entities;

public class GroupDecorator : IEquatable<GroupDecorator>
{
    private readonly Group _group;

    public GroupDecorator(Group group, Schedule schedule)
    {
        _group = group ?? throw new ArgumentNullException(nameof(group));
        Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
    }

    public Schedule Schedule { get; }
    public GroupName Name => _group.Name;

    public override bool Equals(object? obj)
    {
        return Equals(obj as GroupDecorator);
    }

    public bool Equals(GroupDecorator? other)
    {
        return other is not null && _group.Equals(other._group);
    }

    public override int GetHashCode()
    {
        return _group.GetHashCode();
    }
}