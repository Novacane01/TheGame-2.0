using Godot;

namespace Player {
    public class Airbourne : PlayerState {
        public Airbourne(Player player) {
            this.Player = player;
        }
        public override void Enter() {
            GD.Print("Switched to airbourne state");
        }
        public override void PhysicsUpdate(float delta) {

            Player.Velocity = Player.MoveAndSlide(
                Player.Velocity, Vector2.Up, true
            );
        }

        public override void Update(float delta) {
            var playerDir = Player.InputDirection;
            // allows change of direction while in air, simulatied by a state similar to Run state
            if (playerDir.x != 0.0f && Player.Action == global::Player.Action.None) {
                Player.AnimationTree.Set("parameters/StateMachine/Idle/blend_position", playerDir.x);
                Player.AnimationTree.Set("parameters/Attack/blend_position", playerDir.x);
                Player.AnimationTree.Set("parameters/StateMachine/Run/blend_position", playerDir.x);
                Player.AnimationState.Travel("Run");
            }

            Player.Velocity = Player.Velocity.MoveToward(
                new Vector2(playerDir.x * Player.MAX_SPEED, Player.Velocity.y),
                Player.ACCELERATION * delta
            );
            Player.Velocity.y += Player.GRAVITY * delta;
            Player.Velocity.y = Mathf.Clamp(Player.Velocity.y, Player.Velocity.y, Player.TERMINAL_VELOCITY);
        }
    }
}

