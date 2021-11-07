using Godot;
public class Running : OnGround {
    public Running(Player player) {
        this.player = player;
    }
    public override void Enter() {
        GD.Print("Switched to running state");
        player.animationState.Travel("Run");
    }
    public override void PhysicsUpdate(float delta) {
        player.velocity = player.MoveAndSlideWithSnap(
            player.velocity, Vector2.Down, Vector2.Up, true
        );
    }

    public override void Update(float delta) {
        if (player.action == Player.Action.None) {
            var playerDir = player.inputDirection;
            player.animationTree.Set("parameters/StateMachine/Idle/blend_position", playerDir.x);
            player.animationTree.Set("parameters/Attack/blend_position", playerDir.x);
            player.animationTree.Set("parameters/StateMachine/Run/blend_position", playerDir.x);
            player.velocity = player.velocity.MoveToward(
                new Vector2(playerDir.x * player.MAX_SPEED, player.velocity.y),
                player.ACCELERATION * delta
            );
        } else {
            player.velocity = Vector2.Zero;
        }
    }

    public override void UnhandledInput(InputEvent _event) {
        base.UnhandledInput(_event);
    }
}

