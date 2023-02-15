using Godot;

namespace DungeonFramework.Nodes;

public partial class DungeonCellGizmo : EditorNode3DGizmoPlugin
{
    public DungeonCellGizmo()
    {
        // `rebeccapurple`
        CreateMaterial("main", new Color(102f / 255, 51f / 255, 153f / 255));
    }

    public override string _GetGizmoName() => "Dungeon Cell";

    public override bool _HasGizmo(Node3D forNode3D) => forNode3D is DungeonCell;

    public override void _Redraw(EditorNode3DGizmo gizmo)
    {
        gizmo.Clear();

        var lines = new Vector3[]
        {
            // North bottom
            new(-1, 0, -1),
            new( 1, 0, -1),

            // South bottom
            new(-1, 0, 1),
            new( 1, 0, 1),

            // East bottom
            new(1, 0, -1),
            new(1, 0,  1),

            // West bottom
            new(-1, 0, -1),
            new(-1, 0,  1),

            // North top
            new(-1, 2, -1),
            new( 1, 2, -1),

            // South top
            new(-1, 2, 1),
            new( 1, 2, 1),

            // East top
            new(1, 2, -1),
            new(1, 2,  1),

            // West top
            new(-1, 2, -1),
            new(-1, 2,  1),

            // North-east
            new(1, 0, -1),
            new(1, 2, -1),

            // North-west
            new(-1, 0, -1),
            new(-1, 2, -1),

            // South-east
            new(1, 0, 1),
            new(1, 2, 1),

            // South-west
            new(-1, 0, 1),
            new(-1, 2, 1),
        };

        gizmo.AddLines(lines, GetMaterial("main", gizmo), false);
    }
}
