using DungeonFramework.Nodes;

public partial class RevealDoorEffect : DungeonEventEffect
{
    private DungeonCell _cell;

    public override void _Ready()
    {
        _cell = GetNode<DungeonCell>("%DungeonCell6");
    }

    public override void OnActivated()
    {
        _cell.EnabledFaces &= ~DungeonCell.Face.West;

        EmitSignal(SignalName.Completed);
    }
}
