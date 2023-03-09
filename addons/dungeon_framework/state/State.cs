using Godot;
using Godot.Collections;

namespace DungeonFramework.State;

[Tool]
public abstract partial class State : Node
{
    internal StateMachine StateMachine { get; set; }

    [Export]
    internal NodePath[] ValidNextStates { get; set; } = System.Array.Empty<NodePath>();
    public abstract void Enter(Dictionary<Variant, Variant> message);
    public abstract void Update(double delta);
    public abstract void PhysicsUpdate(double delta);
    public abstract void Exit();
}
