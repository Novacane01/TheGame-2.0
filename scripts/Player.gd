extends KinematicBody2D


# Declare member variables here.
const ACCELERATION: float = 10.0;
const MAX_SPEED: float = 100.0;
const FRICTION: float = 10.0;
const GRAVITY: float = 5.0;
const JUMP_POWER: float = 100.0;

var velocity: Vector2 = Vector2.ZERO;
onready var sprite: Sprite = $Sprite;

# Called during the physics processing step of the main loop.
# Physics processing means that the frame rate is synced to the physics, i.e. the delta variable should be constant. delta is in seconds.
func _physics_process(_delta):
	var target_movement: Vector2 = Vector2.ZERO;
	target_movement.x = Input.get_action_strength("move_right") - Input.get_action_strength("move_left");
	
	# Only allow player to jump while on the floor
	if is_on_floor():
		target_movement.y = -Input.get_action_strength("jump");
		velocity.y = target_movement.y * JUMP_POWER;
	
	if target_movement != Vector2.ZERO:
		sprite.flip_h = target_movement.x < 0.0;
		velocity = velocity.move_toward(Vector2(target_movement.x * MAX_SPEED, velocity.y), ACCELERATION);
	else:
		velocity = velocity.move_toward(Vector2(0.0, velocity.y), FRICTION)

	# Simulate real gravity; constant force being applied
	velocity.y += GRAVITY;
	velocity = move_and_slide(velocity, Vector2(0.0,-1.0), true);
