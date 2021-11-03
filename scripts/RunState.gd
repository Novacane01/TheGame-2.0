class_name RunState
extends OnGround

func enter(msg: Dictionary = {}) -> void:
	print("Switched to running state");
	
func update(delta: float):
	var input_direction: Vector2 = player.get_input_direction();
	
	if (input_direction.x != 0.0):
		player.sprite.flip_h = input_direction.x < 0.0;
		player.velocity = player.velocity.move_toward(Vector2(input_direction.x * player.MAX_SPEED, player.velocity.y), player.ACCELERATION * delta);
		
func physics_update(_delta):
	player.velocity = player.move_and_slide(player.velocity, Vector2.UP, true);

func handle_input (_event: InputEvent) -> void:
	.handle_input(_event);
