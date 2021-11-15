using Godot;
using System;

namespace Enemy {
	public class Enemy : KinematicBody2D {
		[Export]
		public float ACCELERATION = 800.0f;
		[Export]
		public float MAX_SPEED = 10.0f;
		[Export]
		public float FRICTION = 800.0f;
		[Export]
		public float GRAVITY = 600.0f;
		[Export]
		public float TERMINAL_VELOCITY = 100.0f;
		[Export]
		public Vector2 Velocity = Vector2.Zero;

		protected StateMachine StateMachine;
		protected Stats Stats;
		public AnimatedSprite Sprite;
		public WanderController WanderController;
		public object Target = null;


		public override void _Ready() {
			Sprite = GetNode<AnimatedSprite>("Sprite");
			StateMachine = new StateMachine();
			Stats = new Stats();
			Stats.Connect("NoHealth", this, nameof(Destroy));
		}

		public override void _Process(float delta) {
			// GD.Print(velocity);
			Velocity.y += GRAVITY * delta;
			Velocity.y = Mathf.Clamp(Velocity.y, 0, TERMINAL_VELOCITY);
			StateMachine.Process(delta);
		}

		public override void _PhysicsProcess(float delta) {
			StateMachine.PhysicsProcess(delta);
		}

		public void Destroy() {
			GD.Print("deaded");
			QueueFree();
		}
	}
}
