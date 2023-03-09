using Godot;

namespace DungeonFramework.Nodes;

[Tool]
public partial class DialogueDungeonEventEffect : DungeonEventEffect
{
    [Export]
    private PackedScene _scene;

    private SubViewportContainer _container;
    private SubViewport _viewport;

    public override void _Ready()
    {
        base._Ready();
        var containerSize = GetTree().Root.Size;

        _viewport = new SubViewport
        {
            TransparentBg = true,
            Size = containerSize,
        };

        _container = new SubViewportContainer
        {
            Visible = false,
            Size = containerSize,
        };

        _container.AddChild(_viewport);
        AddChild(_container);

        GetTree().Root.SizeChanged += ViewPort_SizeChanged;
    }

    public override void OnActivated()
    {
        GD.Print("Effect triggered");
        var instance = _scene.Instantiate();

        _viewport.AddChild(instance);
        _container.Visible = true;

        if (instance.HasSignal("Completed"))
            instance.Connect("Completed", Callable.From(Scene_Completed));
    }

    private void ViewPort_SizeChanged()
    {
        var size = GetTree().Root.Size;

        if (_container is not null)
        {
            _container.Size = size;
        }
    }

    private void Scene_Completed()
    {
        _container.Visible = false;
        foreach (var child in _viewport.GetChildren())
        {
            child.QueueFree();
        }

        EmitSignal(SignalName.Completed);
    }
}
