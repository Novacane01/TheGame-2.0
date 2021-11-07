using Godot;
using System;

public class Player : KinematicBody2D {
    [Export]
    public float ACCELERATION = 800.0f;
    [Export]
    public float MAX_SPEED = 100.0f;
    [Export]
    public float FRICTION = 800.0f;
    [Export]
    public float GRAVITY = 600.0f;
    [Export]
    public float JUMP_POWER = 150.0f;
    [Export]
    public Vector2 velocity;

    public AnimationPlayer animationPlayer;
    public AnimationTree animationTree;
    public AnimationNodeStateMachinePlayback animationState;

    public enum Action {
        None,
        Attacking
    }

    public Action action = Action.None;

    public Vector2 inputDirection = Vector2.Zero;

    private StateMachine stateMachine;

    public override void _Ready() {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationTree = GetNode<AnimationTree>("AnimationTree");
        animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/StateMachine/playback");
        stateMachine = new StateMachine(this);

        var idle = new Idle(this);
        var running = new Running(this);
        var airbourne = new Airbourne(this);

        Func<bool> isAirbourne = () => !IsOnFloor();
        Func<bool> isRunning = () => inputDirection.x != 0.0f;
        Func<bool> isIdle = () => inputDirection == Vector2.Zero;
        stateMachine.AddAnyTransition(airbourne, isAirbourne);
        stateMachine.AddAnyTransition(running, isRunning);
        stateMachine.AddAnyTransition(idle, isIdle);
        stateMachine.SetState(idle);
    }

    public override void _Process(float delta) {
        stateMachine.Process(delta);
    }

    public override void _PhysicsProcess(float delta) {
        stateMachine.PhysicsProcess(delta);
    }

    public override void _UnhandledInput(InputEvent _event) {
        GetInputDirection();
        if (Input.IsActionJustPressed("attack")) {
            GD.Print("Attacking");
            animationTree.Set("parameters/OneShot/active", true);
            action = Action.Attacking;
        }
        stateMachine.UnhandledInput(_event);
    }
    public void GetInputDirection() {
        inputDirection = new Vector2(
            Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
            Input.GetActionStrength("jump")
        );
    }

    public void actionHasEnded() {
        action = Action.None;
    }
}
