using Godot;
using System;

namespace Enemy {
	public class Wander: EnemyState {
		
		private short _wanderRange = 32;
		private Random _random = new Random();
		public Wander(Enemy enemy) {
			this.Enemy = enemy;
		}
		public override void Enter() {
			var initialPosition = Enemy.WanderController.InitialPosition;
			GD.Print("Switched to Wander state");
			// GD.Print("Initial Position: {}", _initialPosition);
			Enemy.WanderController.TargetPosition = new Vector2(initialPosition.x +  _random.Next(-_wanderRange, _wanderRange), initialPosition.y);
			// GD.Print("Target Position: {}", _targetPosition);
		}


		public override void Update(float delta) {
			var targetPosition = Enemy.WanderController.TargetPosition;
			var direction = Enemy.GlobalPosition.DirectionTo(targetPosition);
			Enemy.Velocity = Enemy.Velocity.MoveToward(direction * Enemy.MAX_SPEED, Enemy.ACCELERATION * delta);
		}

		public override void PhysicsUpdate(float delta) {
			Enemy.Sprite.FlipH = Enemy.Velocity.x < 0.0f;
			Enemy.Velocity = Enemy.MoveAndSlideWithSnap(
				Enemy.Velocity, Vector2.Down, Vector2.Up, true
			);
		}
	}
}
