class_name JumpAttackState
extends PlayerState


func _init(player: Player).(player):
	pass


func enter(_msg: Dictionary = {}) -> void:
	print("Switched to Jump Attack state")
	player.animationState.travel("Attack")
	player.isAttacking = true


func update(delta: float) -> void:
	player.velocity.y += player.GRAVITY * delta
	var player_direction: Vector2 = player.get_input_direction()

	if player_direction.x != 0.0:
		player.velocity = player.velocity.move_toward(
			Vector2(player_direction.x * player.MAX_SPEED, player.velocity.y),
			player.ACCELERATION * delta
		)


func physics_update(_delta):
	player.velocity = player.move_and_slide(player.velocity, Vector2.UP, true)
