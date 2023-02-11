using Godot;

public partial class Player : CharacterBody3D
{
    private Camera3D _camera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _camera = GetNode<Camera3D>("Camera");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // TODO: Handle camera rotation
        if (Input.IsActionJustPressed("move_forward"))
        {
            var collision = MoveAndCollide(Vector3.Forward * 2, testOnly: true, maxCollisions: 12);
            // TODO: Check collision
            Translate(Vector3.Forward * 2);
        }
        else if (Input.IsActionJustPressed("move_backward"))
        {
            // TODO: Check collision
            Translate(Vector3.Back * 2);
        }
        else if (Input.IsActionJustPressed("move_left"))
        {
            RotateObjectLocal(Vector3.Up, Mathf.DegToRad(90));
        }
        else if (Input.IsActionJustPressed("move_right"))
        {
            RotateObjectLocal(Vector3.Up, Mathf.DegToRad(-90));
        }
    }
}
