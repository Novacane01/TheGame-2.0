extends KinematicBody2D

const ACCELERATION: float = 800.0;
const MAX_SPEED: float = 100.0;
const FRICTION: float = 800.0;
const GRAVITY: float = 600.0;
const JUMP_POWER: float = 200.0;

var velocity: Vector2 = Vector2.ZERO;
onready var sprite: Sprite = $Sprite;

func _process(delta):
	var target_movement: Vector2 = Vector2.ZERO;
	target_movement.x = (
		Input.get_action_strength("move_right")
		- Input.get_action_strength("move_left")
	);

	# Only allow player to jump when touching the ground
	if is_on_floor():
		target_movement.y = -Input.get_action_strength("jump");
		velocity.y = target_movement.y * JUMP_POWER;

	if target_movement != Vector2.ZERO:
		sprite.flip_h = target_movement.x < 0.0;
		velocity = velocity.move_toward(Vector2(target_movement.x * MAX_SPEED, velocity.y), ACCELERATION * delta);
	else:
		velocity = velocity.move_toward(Vector2(0.0, velocity.y), FRICTION * delta);

	# Simulate real gravity; constant force being applied
	velocity.y += GRAVITY * delta;

func _physics_process(_delta):
	velocity = move_and_slide(velocity, Vector2(0.0, -1.0), true);
