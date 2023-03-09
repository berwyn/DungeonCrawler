using DungeonFramework.State;
using Godot;

public partial class UiTesting : AspectRatioContainer
{
    [Signal]
    public delegate void CompletedEventHandler();

    private Button _button;
    private StateMachine _stateMachine;

    public override void _Ready()
    {
        _button = GetNode<Button>("%CompleteButton");
        _stateMachine = GetNode<StateMachine>("StateMachine");

        _button.Pressed += Button_OnPressed;
        _stateMachine.Transitioned += StateMachine_Transitioned;
    }

    private void Button_OnPressed()
    {
        _stateMachine.TransitionTo("Complete", new());
    }

    private void StateMachine_Transitioned(string state)
    {
        if (state != "Complete")
            return;

        EmitSignal(SignalName.Completed);
    }
}
