using Godot;
namespace Player {
    public class Running : OnGround {
        public Running(Player player) {
            this.Player = player;
        }
        public override void Enter() {
            base.Enter();
            GD.Print("Switched to running state");
            Player.AnimationState.Travel("Run");
        }
        public override void PhysicsUpdate(float delta) {
            Player.Velocity = Player.MoveAndSlideWithSnap(
                Player.Velocity, Player.snapVector, Vector2.Up, true
            );
        }

        public override void Update(float delta) {
            if (Player.Action == global::Player.Action.None) {
                var playerDir = Player.InputDirection;
                Player.AnimationTree.Set("parameters/StateMachine/Idle/blend_position", playerDir.x);
                Player.AnimationTree.Set("parameters/Attack/blend_position", playerDir.x);
                Player.AnimationTree.Set("parameters/StateMachine/Run/blend_position", playerDir.x);
                Player.Velocity = Player.Velocity.MoveToward(
                    new Vector2(playerDir.x * Player.MAX_SPEED, Player.Velocity.y),
                    Player.ACCELERATION * delta
                );
            } else {
                Player.Velocity = Vector2.Zero;
            }
        }

        public override void UnhandledInput(InputEvent _event) {
            base.UnhandledInput(_event);
        }
    }
}

