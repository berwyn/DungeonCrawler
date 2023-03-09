using System;
using System.Collections.Generic;
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
    public Player Player;

    private DungeonEventTrigger _trigger;
    private DungeonEventEffect _effect;
    private MeshInstance3D _indicatorNodeMesh;

    public override void _Ready()
    {
        base._Ready();

        foreach (var child in GetChildren())
        {
            if (child is DungeonEventTrigger dt)
            {
                _trigger = dt;
                if (!_trigger.IsActivated)
                    Visible = false;
            }
            else if (child is DungeonEventEffect de)
            {
                _effect = de;
            }
        }

        CreateTree();
    }

    public override string[] _GetConfigurationWarnings()
    {
        var errors = new List<string>();

        bool hasTrigger = false;
        bool hasEffect = false;

        foreach (var child in GetChildren())
        {
            if (child is DungeonEventTrigger)
                hasTrigger = true;
            else if (child is DungeonEventEffect)
                hasEffect = true;
        }

        if (!hasTrigger)
            errors.Add("No event trigger configured!");

        if (!hasEffect)
            errors.Add("No event effect configured!");

        return errors.ToArray();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public void ActivateEvent()
    {
        if (_trigger is not null && _trigger.RequiresmentsSatisfied)
        {
            _trigger.OnTriggered();
            _effect?.OnActivated();

            Visible = _trigger.IsActivated;
            if (!Visible)
            {
                _indicatorNodeMesh.ProcessMode = ProcessModeEnum.Disabled;
            }
        }
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

        _indicatorNodeMesh.CreateConvexCollision(clean: true, simplify: true);

        AddChild(_indicatorNodeMesh, @internal: InternalMode.Front);
    }
}
