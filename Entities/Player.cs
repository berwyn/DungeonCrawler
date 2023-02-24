using DungeonFramework.Nodes;
using DungeonFramework.Utils;
using Godot;

public partial class Player : Node3D
{
    private Camera3D _camera;
    private Light3D _light;

    private RayCast3D _forward;
    private RayCast3D _reverse;
    private RayCast3D _left;
    private RayCast3D _right;

    private Tween _movementTween;

    public override void _Ready()
    {
        base._Ready();

        _camera = GetNode<Camera3D>("Camera");
        _light = GetNode<Light3D>("Light");

        _forward = GetNode<RayCast3D>("Forward");
        _reverse = GetNode<RayCast3D>("Reverse");
        _left = GetNode<RayCast3D>("Left");
        _right = GetNode<RayCast3D>("Right");
    }

    public override void _PhysicsProcess(double delta)
    {
        // TODO: Handle camera rotation
        if (Input.IsActionJustPressed("move_forward"))
        {
            Move(Vector3.Forward, ref _forward);
        }
        else if (Input.IsActionJustPressed("move_backward"))
        {
            Move(Vector3.Back, ref _reverse);
        }
        else if (Input.IsActionJustPressed("move_left"))
        {
            Move(Vector3.Left, ref _left);
        }
        else if (Input.IsActionJustPressed("move_right"))
        {
            Move(Vector3.Right, ref _right);
        }
        else if (Input.IsActionJustPressed("turn_left"))
        {
            Rotate(Mathf.DegToRad(90));
        }
        else if (Input.IsActionJustPressed("turn_right"))
        {
            Rotate(Mathf.DegToRad(-90));
        }
    }

    private void Move(Vector3 direction, ref RayCast3D ray)
    {
        var motionVector = direction * 2;

        if (ray.IsColliding())
        {
            var collider = ray.GetCollider();
            if (collider is DungeonEvent de)
            {
                de.ActivateEvent();
            }
            else if (collider is Node n && n.HasAncestor<DungeonEvent>())
            {
                n.GetAncestorOrNull<DungeonEvent>()?.ActivateEvent();
            }

            return;
        }

        if (_movementTween?.IsRunning() ?? false)
            return;

        _movementTween = CreateTween();
        _movementTween.TweenMethod(
            Callable.From<Transform3D>(transform => Transform = transform),
            Transform,
            Transform.TranslatedLocal(motionVector),
            0.25f
        )
        .SetTrans(Tween.TransitionType.Linear)
        .SetEase(Tween.EaseType.Out);
    }

    private void Rotate(float angle)
    {
        if (_movementTween?.IsRunning() ?? false)
            return;

        _movementTween = CreateTween();
        _movementTween.TweenMethod(
            Callable.From<Transform3D>(transform => Transform = transform),
            Transform,
            Transform.RotatedLocal(Vector3.Up, angle),
            0.25f
        )
        .SetTrans(Tween.TransitionType.Linear)
        .SetEase(Tween.EaseType.Out);
    }
}
