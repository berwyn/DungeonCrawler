using Godot;

namespace DungeonFramework.Utils;

public static class NodeExtensions
{
    public static bool HasAncestor<T>(this Node node)
    where
        T : Node
    {
        var parent = node.GetParentOrNull<Node>();

        while (parent is not null)
        {
            if (parent is T)
                return true;

            Node current = parent;
            parent = current.GetParentOrNull<Node>();
        }

        return false;
    }

    public static T GetAncestorOrNull<T>(this Node node)
    where
        T : Node
    {
        var parent = node.GetParentOrNull<Node>();

        while (parent is not null)
        {
            if (parent is T value)
                return value;

            parent = parent.GetParentOrNull<Node>();
        }

        return null;
    }
}
