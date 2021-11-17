using Godot;

public class Health : Node {

    [Export]
    public double MAX_HEALTH = 100.0;
    [Signal]
    public delegate void NoHealth();

    protected double health;

    public override void _Ready() {
        health = MAX_HEALTH;
    }

    public virtual double Current {
        get { return health; }
        set {
            health = value;

            if (health <= 0) {
                EmitNoHealthSignal();
            }
        }
    }

    protected void EmitNoHealthSignal() {
        EmitSignal("NoHealth");
    }
}
