using Godot;
using System;
public class Running : OnGround {
    public Running(Player player) {
        this.player = player;
    }
    public override void Enter() {
        GD.Print("Switched to running state");
        player.animationState.Travel("Running");
    }
    public override void PhysicsUpdate(float delta) {
        player.velocity = player.MoveAndSlideWithSnap(
            player.velocity, Vector2.Down, Vector2.Up, true
        );
    }

    public override void Update(float delta) {
        var playerDir = player.inputDirection;
        player.animationTree.Set("parameters/Idle/blend_position", player.inputDirection);
		player.animationTree.Set("parameters/Running/blend_position", playerDir);
        player.velocity = player.velocity.MoveToward(
			new Vector2(playerDir.x * player.MAX_SPEED, player.velocity.y),
			player.ACCELERATION * delta
		);
    }

    public override void UnhandledInput(InputEvent _event){
        base.UnhandledInput(_event);
    }
}   

