class_name AirState
extends PlayerState

var previous_state: String = "";

func enter(_msg: Dictionary = {}) -> void:
	print("Switched to Air state");
	
#	previous_state = _msg["previous"];

func update(delta: float) -> void:
	player.velocity.y += player.GRAVITY * delta;
		
func physics_update(delta: float) -> void:
	player.velocity = player.move_and_slide(player.velocity, Vector2.UP, true);
