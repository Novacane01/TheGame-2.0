using Godot;
using System;
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
        // GD.Print(player.velocity);
        var playerDir = player.inputDirection;
        player.velocity = player.velocity.MoveToward(
			new Vector2(playerDir.x * player.MAX_SPEED, player.velocity.y),
			player.ACCELERATION * delta
		);
    }
}   

