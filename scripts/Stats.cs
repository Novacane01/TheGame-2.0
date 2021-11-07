using Godot;

public class Stats : Node {

	[Export]
	public double MAX_HEALTH = 100.0;

	public double health = 0.0;
	public override void _Ready() {
		health = MAX_HEALTH;
	}
}
