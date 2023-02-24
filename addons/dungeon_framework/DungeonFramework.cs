#if TOOLS
using DungeonFramework.Nodes;
using Godot;

namespace DungeonFramework;

[Tool]
public partial class DungeonFramework : EditorPlugin
{
    private DungeonCellGizmo _dungeonCellGizmo = new();

    public override void _EnterTree()
    {
        var dungeonCell = GD.Load<Script>("res://addons/dungeon_framework/nodes/DungeonCell.cs");
        var dungeonCellIcon = GD.Load<Texture2D>("res://addons/dungeon_framework/nodes/DungeonCell.svg");

        var dungeonEvent = GD.Load<Script>("res://addons/dungeon_framework/nodes/DungeonEvent.cs");
        var dungeonEventIcon = GD.Load<Texture2D>("res://addons/dungeon_framework/nodes/DungeonCell.svg");

        AddCustomType(nameof(DungeonCell), nameof(Node3D), script: dungeonCell, icon: dungeonCellIcon);
        AddCustomType(nameof(DungeonEvent), nameof(Node3D), script: dungeonEvent, icon: dungeonEventIcon);

        var dungeonEventTrigger = GD.Load<Script>("res://addons/dungeon_framework/nodes/DungeonEventTrigger.cs");
        var dungeonEventTriggerIcon = GD.Load<Texture2D>("res://addons/dungeon_framework/nodes/DungeonCell.svg");

        var oneshotDungeonEventTrigger = GD.Load<Script>("res://addons/dungeon_framework/nodes/OneshotDungeonEventTrigger.cs");
        var oneshotDungeonEventTriggerIcon = GD.Load<Texture2D>("res://addons/dungeon_framework/nodes/DungeonCell.svg");

        AddCustomType(nameof(DungeonEventTrigger), nameof(Node), script: dungeonEventTrigger, icon: dungeonEventTriggerIcon);
        AddCustomType(nameof(OneshotDungeonEventTrigger), nameof(Node), script: oneshotDungeonEventTrigger, icon: oneshotDungeonEventTriggerIcon);

        AddNode3DGizmoPlugin(_dungeonCellGizmo);
    }

    public override void _ExitTree()
    {
        RemoveNode3DGizmoPlugin(_dungeonCellGizmo);
    }
}
#endif
