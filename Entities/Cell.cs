using Godot;

public partial class Cell : Node3D
{
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
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        UpdateFaces();
    }

    public void UpdateFaces()
    {
        SetChildVisibility(false, ref _eastFace);
        SetChildVisibility(false, ref _westFace);
    }

    private void SetChildVisibility(bool visible, ref Node node)
    {
        var isParent = this == node.GetParent();

        if (visible && !isParent)
        {
            AddChild(node);
        }
        else if (!visible && isParent)
        {
            RemoveChild(node);
        }
    }
}
