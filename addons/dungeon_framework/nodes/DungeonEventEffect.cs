using Godot;

namespace DungeonFramework.Nodes;

[Tool]
public abstract partial class DungeonEventEffect : Node
{
    [Signal]
    public delegate void CompletedEventHandler();

    public abstract void OnActivated();
}
