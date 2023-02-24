using Godot;

namespace DungeonFramework.Nodes;

[Tool]
public partial class OneshotDungeonEventTrigger : DungeonEventTrigger
{
    // TODO: Create some sort of event ID system and check save state for one-shots
    private bool _hasBeenUsed = false;

    public override bool IsActivated => !_hasBeenUsed;

    public override bool RequiresmentsSatisfied => !_hasBeenUsed;

    public override void OnTriggered()
    {
        _hasBeenUsed = true;
    }
}
