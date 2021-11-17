using Godot;

public class Tree : StaticBody2D {
	private FlashTimer flashTimer;

	public override void _Ready() {
		flashTimer = GetNode<FlashTimer>("FlashTimer");

		// allows Tree to know when its health is depleted
		GetNode<Health>("Health").Connect("NoHealth", this, nameof(_onNoHealth));
	}

	public void _onHurtBoxAreaEntered(Hitbox hitbox) {
		flashTimer.flash();
	}

	private void _onNoHealth() {
		QueueFree();
	}
}
