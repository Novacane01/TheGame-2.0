class_name AirState
extends PlayerState


func enter(_msg: Dictionary = {}) -> void:
	print("Switched to Air state")


func update(delta: float) -> void:
	update_movement(delta)
	player.velocity.y += player.GRAVITY * delta


func physics_update(_delta: float) -> void:
	player.velocity = player.move_and_slide(player.velocity, Vector2.UP, true)
