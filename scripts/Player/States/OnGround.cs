using Godot;
namespace Player {
    public abstract class OnGround : PlayerState {
        public override void Enter() {
            Player.snapVector = Vector2.Down * Player.snapLength;
        }
        public override void UnhandledInput(InputEvent _event) {
            if (Input.IsActionJustPressed("jump")) {
                Player.Velocity.y = -Player.JUMP_POWER;
                Player.snapVector = Vector2.Zero;
            }
        }
    }
}
