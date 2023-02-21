using Godot;

namespace DungeonFramework.Nodes;

[Tool]
public partial class DungeonEvent : Node3D
{
    private Texture2D _indicator;

    [Export]
    public Texture2D Indicator
    {
        get => _indicator;
        set
        {
            _indicator = value;
            CreateTree();
        }
    }

    [Export]
    public DungeonEventTrigger Trigger;

    [Export]
    public Player Player;

    private MeshInstance3D _indicatorNodeMesh;

    public override void _Ready()
    {
        base._Ready();

        CreateTree();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    private void CreateTree()
    {
        _indicatorNodeMesh?.QueueFree();
        _indicatorNodeMesh = new MeshInstance3D
        {
            Name = "Indicator",
            Transform = Transform3D.Identity.TranslatedLocal(new(0, 1, 0)),
            Mesh = new PlaneMesh
            {
                Size = new(DungeonCell.SIZE / 2, DungeonCell.SIZE / 2),
                Orientation = PlaneMesh.OrientationEnum.Z,
            },
            MaterialOverride = new StandardMaterial3D
            {
                AlbedoTexture = Indicator,
                BillboardMode = BaseMaterial3D.BillboardModeEnum.Enabled,
            }
        };

        AddChild(_indicatorNodeMesh, @internal: InternalMode.Front);
    }
}

public abstract partial class DungeonEventTrigger : Resource
{
    public abstract bool IsActivated { get; }
    public abstract bool RequiresmentsSatisfied { get; }
}
