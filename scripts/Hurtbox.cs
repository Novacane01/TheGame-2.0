using Godot;

public class Hurtbox : Area2D {

	[Export]
	public NodePath healthNodePath;

	[Export]
	public double damageModifier = 1.0;

	Health health;

	public override void _Ready() {
		health = GetNode<Health>(healthNodePath);
	}

	private void _on_HurtBox_area_entered(Hitbox hitbox) {
		health.Current -= hitbox.damage * damageModifier;
	}
}
