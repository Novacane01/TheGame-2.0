using Godot;

public class Tree : StaticBody2D {
    private FlashTimer flashTimer;

    public override void _Ready() {
        flashTimer = GetNode<FlashTimer>("FlashTimer");
    }

    public void _on_HurtBox_area_entered(Hitbox hitbox) {
        GD.Print("BRUH2");
        flashTimer.flash();
    }

    private void _onStatsNoHealth() {
        QueueFree();
    }
}
