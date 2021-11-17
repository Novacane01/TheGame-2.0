using Godot;

public class Hurtbox : Area2D {

    [Export]
    public NodePath healthNodePath;

    [Export]
    public double damageModifier = 1.0;

    private Health health;

    public override void _Ready() {
        health = GetNode<Health>(healthNodePath);

        // godot-defined Area2D signal "area_entered" is triggered when collider enters the hurtbox area
        Connect("area_entered", this, nameof(_onHurtBoxAreaEntered));
    }

    private void _onHurtBoxAreaEntered(Hitbox hitbox) {
        health.Current -= hitbox.damage * damageModifier;
    }
}