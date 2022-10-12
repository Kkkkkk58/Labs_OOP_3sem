using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class GroupDecorator : IEquatable<GroupDecorator>
{
    public GroupDecorator(Group groupDecoratee, Schedule schedule)
    {
        GroupDecoratee = groupDecoratee;
        Schedule = schedule;
    }

    public Group GroupDecoratee { get; }
    public Schedule Schedule { get; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as GroupDecorator);
    }

    public bool Equals(GroupDecorator? other)
    {
        return other is not null && GroupDecoratee.Equals(other.GroupDecoratee);
    }

    public override int GetHashCode()
    {
        return GroupDecoratee.GetHashCode();
    }
}