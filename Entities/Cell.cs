using System;
using System.Security.Cryptography.X509Certificates;
using Godot;

[Flags]
public enum CellFaceEnabledFlags
{
    None = 0,
    North = 1 << 0,
    South = 1 << 1,
    East = 1 << 2,
    West = 1 << 3,
    Top = 1 << 4,
    Bottom = 1 << 5,

    All = North | South | East | West | Top | Bottom,
}

public partial class Cell : Node3D
{
    [Export(PropertyHint.Flags)]
    private CellFaceEnabledFlags _enabledFaces = CellFaceEnabledFlags.All;

    private Node _northFace;
    private Node _southFace;
    private Node _eastFace;
    private Node _westFace;
    private Node _topFace;
    private Node _bottomFace;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _northFace = GetNode("NorthFace");
        _southFace = GetNode("SouthFace");
        _eastFace = GetNode("EastFace");
        _westFace = GetNode("WestFace");
        _topFace = GetNode("TopFace");
        _bottomFace = GetNode("BottomFace");

        UpdateFaces();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        UpdateFaces();
    }

    public void UpdateFaces()
    {
        SetChildVisibility(_enabledFaces.HasFlag(CellFaceEnabledFlags.North), ref _northFace);
        SetChildVisibility(_enabledFaces.HasFlag(CellFaceEnabledFlags.South), ref _southFace);
        SetChildVisibility(_enabledFaces.HasFlag(CellFaceEnabledFlags.East), ref _eastFace);
        SetChildVisibility(_enabledFaces.HasFlag(CellFaceEnabledFlags.West), ref _westFace);
        SetChildVisibility(_enabledFaces.HasFlag(CellFaceEnabledFlags.Top), ref _topFace);
        SetChildVisibility(_enabledFaces.HasFlag(CellFaceEnabledFlags.Bottom), ref _bottomFace);
    }

    private void SetChildVisibility(bool visible, ref Node node)
    {
        var isParent = this == node.GetParent();

        if (visible && !isParent)
        {
            AddChild(node);
            node.GetNode<CollisionShape3D>("StaticBody3D/CollisionShape3D").Disabled = false;
        }
        else if (!visible && isParent)
        {
            node.GetNode<CollisionShape3D>("StaticBody3D/CollisionShape3D").Disabled = true;
            RemoveChild(node);
        }
    }
}
