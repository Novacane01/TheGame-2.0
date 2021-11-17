using Godot;
using System;

namespace Enemy {
    public class Idle: EnemyState {
        private Vector2 _initialPosition = Vector2.Zero;
        private Random _random = new Random();
        public Idle(Enemy enemy) {
            this.Enemy = enemy;
            _initialPosition = enemy.GlobalPosition;
        }
        public override void Enter() {
            GD.Print("Switched to Idle state");
            Enemy.WanderController.StartTimer(_random.Next(1,3));
        }


        public override void Update(float delta) {
            // GD.Print(Enemy.WanderController.GetTimeLeft());
            Enemy.Velocity = Enemy.Velocity.MoveToward(Vector2.Zero, Enemy.FRICTION * delta);
        }

        public override void PhysicsUpdate(float delta) {
            Enemy.Velocity = Enemy.MoveAndSlideWithSnap(
                Enemy.Velocity, Vector2.Down, Vector2.Up, true
            );
        }
    }
}
