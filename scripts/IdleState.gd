class_name IdleState
extends OnGround

func enter(_msg: Dictionary = {}) -> void:
  print("Switched to Idle state");
  player.animationState.travel("Idle");

func update(delta: float) -> void:
	player.velocity = player.velocity.move_toward(Vector2(0.0, player.velocity.y), player.FRICTION * delta);

func physics_update(_delta):
	player.velocity = player.move_and_slide(player.velocity, Vector2.UP, true);
	
func handle_input (_event: InputEvent) -> void:
	.handle_input(_event);
