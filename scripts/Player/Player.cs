using Godot;
using System;

namespace Player {
	public enum Action {
		None,
		Attacking
	}
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
		public float snapLength = 30.0f;
		[Export]
		public float JUMP_POWER = 150.0f;
		[Export]
		public float TERMINAL_VELOCITY = 100.0f;
		[Export]
		public Vector2 Velocity = Vector2.Zero;

		public Vector2 snapVector = Vector2.Down;

		public AnimationPlayer AnimationPlayer;
		public AnimationTree AnimationTree;
		public AnimationNodeStateMachinePlayback AnimationState;

		public Action Action = Action.None;

		public Vector2 InputDirection = Vector2.Zero;

		private StateMachine _stateMachine;

		public override void _Ready() {
			AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
			AnimationTree = GetNode<AnimationTree>("AnimationTree");
			AnimationState = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/StateMachine/playback");
			_stateMachine = new StateMachine();

			var idle = new Idle(this);
			var running = new Running(this);
			var airbourne = new Airbourne(this);

			Func<bool> isAirbourne = () => !IsOnFloor();
			Func<bool> isRunning = () => InputDirection.x != 0.0f;
			Func<bool> isIdle = () => InputDirection == Vector2.Zero;
			_stateMachine.AddAnyTransition(airbourne, isAirbourne);
			_stateMachine.AddAnyTransition(running, isRunning);
			_stateMachine.AddAnyTransition(idle, isIdle);
			_stateMachine.SetState(idle);
		}

		public override void _Process(float delta) {
			_stateMachine.Process(delta);
		}

		public override void _PhysicsProcess(float delta) {
			_stateMachine.PhysicsProcess(delta);
		}

		public override void _UnhandledInput(InputEvent _event) {
			GetInputDirection();
			if (Input.IsActionJustPressed("attack")) {
				GD.Print("Attacking");
				AnimationTree.Set("parameters/OneShot/active", true);
				Action = Action.Attacking;
			}
			_stateMachine.UnhandledInput(_event);
		}
		public void GetInputDirection() {
			InputDirection = new Vector2(
				Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
				Input.GetActionStrength("jump")
			);
		}

		public void actionHasEnded() {
			Action = Action.None;
		}
	}
}
