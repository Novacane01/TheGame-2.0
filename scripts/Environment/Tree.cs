using Godot;

public class Tree : StaticBody2D {
	private Stats stats;
	private Sprite sprite;
	private Timer flashTimer;
	public override void _Ready() {
		stats = GetNode<Stats>("Stats");
		sprite = GetNode<Sprite>("Sprite");
		flashTimer = GetNode<Timer>("FlashTimer");
	}

	public void flash() {
		(sprite.Material as ShaderMaterial).SetShaderParam("flash_modifier", 1);
		flashTimer.Start();
	}

	public void _on_HurtBox_area_entered(Area2D _area) {
		stats.Health -= 10.0f;
		flash();

		if (stats.Health <= 0.0) {
			QueueFree();
		}
	}

	private void flashEnded() {
		(sprite.Material as ShaderMaterial).SetShaderParam("flash_modifier", 0);

	}
}
