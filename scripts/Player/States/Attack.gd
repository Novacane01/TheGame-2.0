class_name AttackState
extends PlayerState


func _init(player: Player).(player):
	pass


func enter(_msg: Dictionary = {}) -> void:
	print("Switched to Attack state")
	player.animationState.travel("Attack")
	player.isAttacking = true


func update(delta: float) -> void:
	player.velocity = player.velocity.move_toward(Vector2.ZERO, player.FRICTION * delta)


func physics_update(_delta):
	player.velocity = player.move_and_slide_with_snap(
		player.velocity, Vector2.DOWN, Vector2.UP, true
	)
