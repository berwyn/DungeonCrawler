#if TOOLS
using DungeonFramework.Nodes;
using DungeonFramework.State;
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

        var dungeonEventEffect = GD.Load<Script>("res://addons/dungeon_framework/nodes/DungeonEventEffect.cs");
        var dungeonEventEffectIcon = GD.Load<Texture2D>("res://addons/dungeon_framework/nodes/DungeonCell.svg");
        var dialogueDungeonEventEffect = GD.Load<Script>("res://addons/dungeon_framework/nodes/DialogueDungeonEventEffect.cs");
        var dialogueDungeonEventEffectIcon = GD.Load<Texture2D>("res://addons/dungeon_framework/nodes/DungeonCell.svg");

        AddCustomType(nameof(DungeonEventEffect), nameof(Node), script: dungeonEventEffect, icon: dungeonEventEffectIcon);
        AddCustomType(nameof(DialogueDungeonEventEffect), nameof(Node), script: dialogueDungeonEventEffect, icon: dialogueDungeonEventEffectIcon);

        var stateMachine = GD.Load<Script>("res://addons/dungeon_framework/state/StateMachine.cs");
        var stateMachineIcon = GD.Load<Texture2D>("res://addons/dungeon_framework/nodes/DungeonCell.svg");
        var state = GD.Load<Script>("res://addons/dungeon_framework/state/State.cs");
        var stateIcon = GD.Load<Texture2D>("res://addons/dungeon_framework/nodes/DungeonCell.svg");

        AddCustomType(nameof(StateMachine), nameof(Node), script: stateMachine, icon: stateMachineIcon);
        AddCustomType(nameof(State), nameof(Node), script: state, icon: stateIcon);

        AddNode3DGizmoPlugin(_dungeonCellGizmo);
    }

    public override void _ExitTree()
    {
        RemoveNode3DGizmoPlugin(_dungeonCellGizmo);
    }
}
#endif
