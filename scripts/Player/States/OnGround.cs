using Godot;
public abstract class OnGround : PlayerState {
    public override void UnhandledInput(InputEvent _event) {
        if (Input.IsActionJustPressed("jump")) {
            player.velocity.y = -player.JUMP_POWER;
        }
    }
}