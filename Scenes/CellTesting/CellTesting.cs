using DungeonFramework.Nodes;
using Godot;

public partial class CellTesting : Node3D
{
    private Player _player;
    private DungeonEvent _dialogueEvent;
    private DungeonEvent _wallEvent;

    public override void _Ready()
    {
        _player = GetNode<Player>("Player");

        _dialogueEvent = GetNode<DungeonEvent>("DungeonEvent");
        _dialogueEvent.ConnectEventHandlers(
            OnActivated: Callable.From(DungeonEvent_OnActivated),
            OnComplete: Callable.From(DungeonEvent_OnCompleted)
        );

        _wallEvent = GetNode<DungeonEvent>("DungeonEvent2");
        _wallEvent.ConnectEventHandlers(
            OnActivated: Callable.From(DungeonEvent_OnActivated),
            OnComplete: Callable.From(DungeonEvent_OnCompleted)
        );
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void DungeonEvent_OnActivated()
    {
        _player.IsMovementAllowed = false;
    }

    private void DungeonEvent_OnCompleted()
    {
        _player.IsMovementAllowed = true;
    }
}
