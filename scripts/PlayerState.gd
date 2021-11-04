class_name PlayerState
extends State

var player: Player = null


func _ready():
	yield(owner, "ready")
	player = owner as Player
	assert(player != null)


func update_movement(delta):
	var player_direction: Vector2 = player.get_input_direction()

	if player_direction.x != 0.0:
		player.animationTree.set("parameters/Idle/blend_position", player_direction)
		player.animationTree.set("parameters/Run/blend_position", player_direction)
		player.animationState.travel("Run")
		player.velocity = player.velocity.move_toward(
			Vector2(player_direction.x * player.MAX_SPEED, player.velocity.y),
			player.ACCELERATION * delta
		)
