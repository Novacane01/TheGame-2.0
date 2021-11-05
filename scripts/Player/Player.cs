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

	public bool isAttacking = false;

	public Vector2 inputDirection = Vector2.Zero;
	
	private StateMachine stateMachine;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		stateMachine = new StateMachine(this);

		var idle = new Idle(this);
		var running = new Running(this);
		var airbourne = new Airbourne(this);
		var attacking = new Attacking(this);

		Func<bool> isAirbourne = () => !IsOnFloor();
		Func<bool> isRunning = () => Math.Abs(inputDirection.x) > 0.0f;
		Func<bool> isIdle = () => inputDirection == Vector2.Zero;
		stateMachine.AddAnyTransition(airbourne, isAirbourne);
		stateMachine.AddAnyTransition(running, isRunning);
		stateMachine.AddAnyTransition(idle, isIdle);
		stateMachine.SetState(idle);
		// Func<bool> isIdle() => () => GetInputDirection().x > 0;
		// Func<bool> isRunning() => () => GetInputDirection().x > 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta) {
		stateMachine.Process(delta);
	}

	public override void _PhysicsProcess(float delta) {
		stateMachine.PhysicsProcess(delta);
	}

	 public override void _UnhandledInput(InputEvent _event) {
		 GetInputDirection();
		 if (Input.IsActionJustPressed("attack")) {
			string previousAnimation = animationPlayer.CurrentAnimation;
			animationState.Travel("Attacking");
			animationTree.Set("parameters/Attack/blend_position", inputDirection);
			animationPlayer.Connect("animation_finished", this, "fuck");
			// animationState.Travel(previousAnimation);
			GD.Print("finished");
		 }
		stateMachine.UnhandledInput(_event);
	}

	public void fuck(string name) {
		GD.Print("FUCK");
	}

	public void GetInputDirection() {
		inputDirection = new Vector2(
			Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			Input.GetActionStrength("jump")
		);
	}
}
