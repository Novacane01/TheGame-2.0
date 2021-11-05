using Godot;
using System;
public class Idle : OnGround {
    public Idle(Player player) {
        this.player = player;
    }
    public override void Enter() {
        GD.Print("Switched to Idle state");
        player.animationState.Travel("Idle");
    }	


    public override void Update(float delta) {
        player.velocity = player.velocity.MoveToward(Vector2.Zero, player.FRICTION * delta);
    }

    public override void PhysicsUpdate(float delta) {
        player.velocity = player.MoveAndSlideWithSnap(
            player.velocity, Vector2.Down, Vector2.Up, true
        );
    }

    public override void UnhandledInput(InputEvent _event){
        base.UnhandledInput(_event);
    }
}
