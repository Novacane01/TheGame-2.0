using Godot;
using System;

namespace Enemy {
    public class Chase: EnemyState {
        private KinematicBody2D _target = null;
        public Chase(Enemy enemy) {
            this.Enemy = enemy;
        }
        public override void Enter() {
            GD.Print("Switched to Chase state");
            _target = (KinematicBody2D)Enemy.Target;
            // player.animationState.Travel("Idle");
        }


        public override void Update(float delta) {
            var dir = Enemy.GlobalPosition.DirectionTo(_target.GlobalPosition);
            Enemy.Velocity = Enemy.Velocity.MoveToward(dir * Enemy.MAX_SPEED, Enemy.ACCELERATION * delta);
            Enemy.Sprite.FlipH = Enemy.Velocity.x < 0.0f;
        }

        public override void PhysicsUpdate(float delta) {
            Enemy.Velocity = Enemy.MoveAndSlideWithSnap(
                Enemy.Velocity, Vector2.Down, Vector2.Up, true
            );
        }
    }
}
