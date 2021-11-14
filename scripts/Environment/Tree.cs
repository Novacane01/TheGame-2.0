using Godot;

public class Tree : StaticBody2D {
	private FlashTimer flashTimer;
	private Health health;

	public override void _Ready() {
		flashTimer = GetNode<FlashTimer>("FlashTimer");
		health = GetNode<Health>("Health");
		health.Connect("NoHealth", this, "_onStatsNoHealth");
	}

	public void _on_HurtBox_area_entered(Hitbox hitbox) {
		flashTimer.flash();
	}

	private void _onStatsNoHealth() {
		QueueFree();
	}
}
