using Godot;

public class Health : Node {

    [Export]
    public double MAX_HEALTH = 100.0;

    [Signal]
    public delegate void NoHealth();
    public string noHealthSignal = nameof(NoHealth);

    private double health;

    public override void _Ready() {
        health = MAX_HEALTH;
        Connect(noHealthSignal, GetParent(), "_onStatsNoHealth");
    }

    public double Current {
        get { return health; }
        set {
            health = value;
            GD.Print("New Health: ", health);

            if (health <= 0) {
                GD.Print("No Health");
                EmitSignal(noHealthSignal);
            }
        }
    }


}