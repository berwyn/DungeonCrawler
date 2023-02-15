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

        AddCustomType(nameof(DungeonCell), nameof(Node3D), script: dungeonCell, icon: dungeonCellIcon);
        AddNode3DGizmoPlugin(_dungeonCellGizmo);
    }

    public override void _ExitTree()
    {
        RemoveNode3DGizmoPlugin(_dungeonCellGizmo);
    }
}
#endif
