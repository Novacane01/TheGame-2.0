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
        protected Node Health;
        public AnimatedSprite Sprite;
        public WanderController WanderController;

        public object Target = null;

        private FlashTimer flashTimer;

        public override void _Ready() {
            Sprite = GetNode<AnimatedSprite>("Sprite");
            flashTimer = GetNode<FlashTimer>("FlashTimer");
            StateMachine = new StateMachine();
            Health = GetNode("Health");
            Health.Connect("NoHealth", this, nameof(_onNoHealth));
        }

        public override void _Process(float delta) {
            Velocity.y += GRAVITY * delta;
            Velocity.y = Mathf.Clamp(Velocity.y, 0, TERMINAL_VELOCITY);
            StateMachine.Process(delta);
        }

        public override void _PhysicsProcess(float delta) {
            StateMachine.PhysicsProcess(delta);
        }

        public void _onHurtBoxAreaEntered(Hitbox hitbox) {
            flashTimer.flash();
        }

        private void _onNoHealth() {
            QueueFree();
        }
    }
}
