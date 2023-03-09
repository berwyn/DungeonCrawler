using System.Linq;
using Godot;
using Godot.Collections;

namespace DungeonFramework.State;

[Tool]
public partial class StateMachine : Node
{
    [Signal]
    public delegate void TransitionedEventHandler(string stateName);

    [Export]
    private NodePath _initialState;

    private State _state;

    public override void _Ready()
    {
        base._Ready();
        GD.Print(_initialState);
        var proposed = GetNode(_initialState);
        if (proposed is State state)
        {
            _state = state;

            foreach (var child in GetChildren())
            {
                if (child is State s)
                    s.StateMachine = this;
            }

            _state.Enter(new());
        }
    }

    public override void _Process(double delta)
        => _state?.Update(delta);

    public override void _PhysicsProcess(double delta)
        => _state?.PhysicsUpdate(delta);

    public void TransitionTo(string targetStateName, Dictionary<Variant, Variant> message)
    {
        GD.Print($"Attempting to transition to state {targetStateName}");
        if (!HasNode(targetStateName))
        {
            GD.Print($"State {targetStateName} not in tree");
            return;
        }

        var node = GetNode(targetStateName);
        if (node is not State next)
        {
            GD.Print($"State {targetStateName} is not a valid state");
            return;
        }

        if (next == _state)
        {
            GD.Print($"State {targetStateName} is already the current state");
            return;
        }

        message ??= new();

        _state.Exit();
        _state = next;
        _state.Enter(message);

        EmitSignal(SignalName.Transitioned, targetStateName);
    }
}
