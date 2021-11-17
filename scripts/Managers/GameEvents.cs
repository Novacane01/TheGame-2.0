using Godot;

public class GameEvents : Node {
    [Signal]
    public delegate void PlayerHealthChanged(Player.PlayerHealth health);

    public void EmitPlayerHealthChanged(Player.PlayerHealth health) {
        EmitSignal(nameof(PlayerHealthChanged), health);
    }
}
