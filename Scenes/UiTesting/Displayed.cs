using System;
using DungeonFramework.State;
using Godot;
using Godot.Collections;

public partial class Displayed : State
{
    public override void Enter(Dictionary<Variant, Variant> message)
    {
        GD.Print("Displayed entered");
    }

    public override void Exit()
    {
        GD.Print("Displayed exited");
    }

    public override void PhysicsUpdate(double delta)
    {
    }

    public override void Update(double delta)
    {
    }
}
