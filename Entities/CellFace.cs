using Godot;

public partial class CellFace : Node3D
{
    private MeshInstance3D _mesh;
    private StaticBody3D _body;
    private CollisionShape3D _collision;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _mesh = GetNode<MeshInstance3D>("MeshInstance3D");
        _body = GetNode<StaticBody3D>("MeshInstance3D/StaticBody3D");
        _collision = GetNode<CollisionShape3D>("MeshInstance3D/StaticBody3D/CollisionShape3D");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void Disable()
    {
        _mesh.Visible = false;
        _body.CollisionLayer = 0;
        _body.CollisionMask = 0;
        GD.Print("Disabling face");
    }

    public void Enable()
    {
        _mesh.Visible = true;
        _body.CollisionLayer = 1;
        _body.CollisionMask = 1;
        GD.Print("Enabling face");
    }
}
