using Godot;

public class FlashTimer : Timer {

	[Export]
	public NodePath spritePath;
	private ShaderMaterial material;
	private float flashModifier = 1.0f;

	public override void _Ready() {

		// This node assumes that the sprite node contains the material with the "flash" shader
		material = GetNode<Node2D>(spritePath).Material as ShaderMaterial;
		flashModifier = (float)material.GetShaderParam("flash_modifier");

		// removes flash effect to begin
		material.SetShaderParam("flash_modifier", 0);
	}

	public void flash() {
		material.SetShaderParam("flash_modifier", flashModifier);
		this.Start();
	}

	// signal connected to timer itself, telling itself to removing the flash shader
	private void _onTimerEnded() {
		material.SetShaderParam("flash_modifier", 0);
	}
}
