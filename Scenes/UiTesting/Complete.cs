using System;
using DungeonFramework.State;
using Godot;
using Godot.Collections;

public partial class Complete : State
{
    public override void Enter(Dictionary<Variant, Variant> message)
    {
        GD.Print("Complete entered");
    }

    public override void Exit()
    {
        GD.Print("Complete exited");
    }

    public override void PhysicsUpdate(double delta)
    {
    }

    public override void Update(double delta)
    {
    }
}
