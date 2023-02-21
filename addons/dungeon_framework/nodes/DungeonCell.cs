using System;
using Godot;

namespace DungeonFramework.Nodes;

[Tool]
public partial class DungeonCell : Node3D
{
    [Flags]
    public enum Face
    {
        North = 1 << 0,
        South = 1 << 1,
        East = 1 << 2,
        West = 1 << 3,
        Top = 1 << 4,
        Bottom = 1 << 5,

        All = North | South | East | West | Top | Bottom,
    }

    /// <summary>
    /// The size of the cells in units.
    /// </summary>
    public const byte SIZE = 2;

    private Face _enabledFaces;

    /// <summary>
    /// The faces that are enabled on this cell. Enabled faces have a visible mesh
    /// and a collision plane.
    /// </summary>
    [Export]
    public Face EnabledFaces
    {
        get => _enabledFaces;
        set
        {
            _enabledFaces = value;
            CreateFaces();
        }
    }

    private Texture2D _texture;

    /// <summary>
    /// The texture to display on visible faces.
    /// </summary>
    [Export]
    public Texture2D Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            CreateFaces();
        }
    }

    private Node3D _north;
    private Node3D _south;
    private Node3D _east;
    private Node3D _west;
    private Node3D _top;
    private Node3D _bottom;

    private void CreateFaces()
    {
        foreach (var child in GetChildren())
        {
            if (child.IsQueuedForDeletion())
                continue;

            RemoveChild(child);
            child.QueueFree();
        }

        CreateFace(Face.North, ref _north);
        CreateFace(Face.South, ref _south);
        CreateFace(Face.East, ref _east);
        CreateFace(Face.West, ref _west);
        CreateFace(Face.Top, ref _top);
        CreateFace(Face.Bottom, ref _bottom);
    }

    private void CreateFace(Face flag, ref Node3D face)
    {
        if (face is not null)
        {
            face.QueueFree();
            face = null;
        }

        if (!_enabledFaces.HasFlag(flag))
            return;

        face = new Node3D()
        {
            Transform = FaceToTransform(flag)
        };

        var mesh = new MeshInstance3D
        {
            Mesh = new PlaneMesh
            {
                Size = new(SIZE, SIZE),
            },
            MaterialOverride = new StandardMaterial3D
            {
                AlbedoTexture = _texture
            },
        };

        mesh.CreateConvexCollision(clean: true, simplify: true);

        AddChild(face);
        face.AddChild(mesh);
    }

    private static Transform3D FaceToTransform(Face face)
    {
        return face switch
        {
            Face.North => Transform3D.Identity
                .TranslatedLocal(new(0, 1, -1))
                .RotatedLocal(new(1, 0, 0), Mathf.DegToRad(90)),

            Face.South => Transform3D.Identity
                .TranslatedLocal(new(0, 1, 1))
                .RotatedLocal(new(1, 0, 0), Mathf.DegToRad(-90)),

            Face.East => Transform3D.Identity
                .TranslatedLocal(new(1, 1, 0))
                .RotatedLocal(new(0, 0, 1), Mathf.DegToRad(90)),

            Face.West => Transform3D.Identity
                .TranslatedLocal(new(-1, 1, 0))
                .RotatedLocal(new(0, 0, 1), Mathf.DegToRad(-90)),

            Face.Top => Transform3D.Identity
                .TranslatedLocal(new(0, 2, 0))
                .RotatedLocal(new(1, 0, 0), Mathf.DegToRad(180)),

            _ => Transform3D.Identity,
        };
    }
}
