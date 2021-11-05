class_name Player
extends KinematicBody2D

export var ACCELERATION: float = 800.0
export var MAX_SPEED: float = 100.0
export var FRICTION: float = 800.0
export var GRAVITY: float = 600.0
export var JUMP_POWER: float = 150.0

var velocity: Vector2 = Vector2.ZERO

onready var animationPlayer = $AnimationPlayer
onready var animationTree = $AnimationTree
# playback determines which state of animation the player is in (i.e. Idle, Run, etc..)
onready var animationState = animationTree.get("parameters/playback")


func _ready() -> void:
	animationTree.active = true


export var isAttacking: bool = false


func get_input_direction() -> Vector2:
	return Vector2(
		Input.get_action_strength("move_right") - Input.get_action_strength("move_left"),
		Input.get_action_strength("jump")
	)
