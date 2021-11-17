using Godot;
using System;

public class HealthBarUI : Control {
    ProgressBar healthBar;
    private GameEvents gameEvents;

    public override void _Ready() {
        healthBar = GetNode<ProgressBar>("ProgressBar");
        gameEvents = GetNode<GameEvents>("/root/GameEvents");

        gameEvents.Connect("PlayerHealthChanged", this, nameof(_onPlayerHealthChanged));
    }

    // updates health and max health, don't care which one changed
    private void _onPlayerHealthChanged(Player.PlayerHealth health) {
        healthBar.MaxValue = health.MAX_HEALTH;
        healthBar.Value = health.Current;
    }

}
