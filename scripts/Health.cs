using Godot;

public class Health : Node {

	[Export]
	public double MAX_HEALTH = 100.0;
	
	[Signal]
	public delegate void NoHealth();

	private double health;

	public override void _Ready() {
		health = MAX_HEALTH;
	}

	public double Current {
		get { return health; }
		set {
			health = value;

			if (health <= 0) {
				EmitNoHealthSignal();
			}
		}
	}

	private void EmitNoHealthSignal() {
		EmitSignal("NoHealth");
	}
}
