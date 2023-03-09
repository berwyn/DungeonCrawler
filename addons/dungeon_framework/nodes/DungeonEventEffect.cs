using Godot;

namespace DungeonFramework.Nodes;

[Tool]
public abstract partial class DungeonEventEffect : Node
{
    public abstract void OnActivated();
}
