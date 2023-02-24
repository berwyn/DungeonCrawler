using Godot;

namespace DungeonFramework.Nodes;

[Tool]
public abstract partial class DungeonEventTrigger : Node
{
    public abstract bool IsActivated { get; }
    public abstract bool RequiresmentsSatisfied { get; }

    public abstract void OnTriggered();
}
