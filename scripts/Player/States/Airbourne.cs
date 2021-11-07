using Godot;
public class Airbourne : PlayerState {
    public Airbourne(Player player) {
        this.player = player;
    }
    public override void Enter() {
        GD.Print("Switched to airbourne state");
    }
    public override void PhysicsUpdate(float delta) {
        player.velocity = player.MoveAndSlideWithSnap(
            player.velocity, Vector2.Down, Vector2.Up, true
        );
    }

    public override void Update(float delta) {
        player.velocity.y += player.GRAVITY * delta;
        var playerDir = player.inputDirection;

        // allows change of direction while in air, simulatied by a state similar to Run state
        if (playerDir.x != 0.0f) {
            player.animationTree.Set("parameters/StateMachine/Idle/blend_position", playerDir.x);
            player.animationTree.Set("parameters/Attack/blend_position", playerDir.x);
            player.animationTree.Set("parameters/StateMachine/Run/blend_position", playerDir.x);
            player.animationState.Travel("Run");
        }

        player.velocity = player.velocity.MoveToward(
            new Vector2(playerDir.x * player.MAX_SPEED, player.velocity.y),
            player.ACCELERATION * delta
        );
    }
}

