class_name OnGround
extends PlayerState


func handle_input(_event: InputEvent) -> void:
	if Input.is_action_just_pressed("jump"):
		player.velocity.y = -player.JUMP_POWER
