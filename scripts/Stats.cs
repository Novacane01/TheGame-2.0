using Godot;

public class Stats : Node {
	[Export]
	public float MAX_HEALTH = 100.0f;
	private float health = 0.0f;

	public float Health {
		get { return health; }
		set {
			if (health <= 0.0f) {
				EmitSignal("NoHealth");
			}
		}
	}
	public override void _Ready() {
		health = MAX_HEALTH;
	}

	[Signal]
	delegate void NoHealth();
}
