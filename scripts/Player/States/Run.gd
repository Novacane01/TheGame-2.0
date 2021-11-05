class_name RunState
extends OnGround


func _init(player: Player).(player):
	pass


func enter(_msg: Dictionary = {}) -> void:
	print("Switched to running state")
	var player_direction = player.get_input_direction()


func update(delta: float):
	update_movement(delta)


func physics_update(_delta):
	player.velocity = player.move_and_slide_with_snap(
		player.velocity, Vector2.DOWN, Vector2.UP, true
	)


func handle_input(_event: InputEvent) -> void:
	.handle_input(_event)
