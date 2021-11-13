using Godot;

public class Hurtbox : Area2D {

	[Export]
	public NodePath healthNodePath;

	Health health;

	public override void _Ready() {
		health = GetNode<Health>(healthNodePath);
	}

	private void _on_HurtBox_area_entered(Hitbox hitbox) {
		GD.Print("BRUH");
		health.Current -= hitbox.damage;
	}

}


