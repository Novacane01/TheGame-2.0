extends KinematicBody2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
const ACCELERATION: float = 10.0;
const MAX_SPEED: float = 100.0;
const FRICTION: float = 10.0;
const GRAVITY: float = 5.0;
const JUMP_POWER: float = 100.0;

var velocity: Vector2 = Vector2.ZERO;
# Called when the node enters the scene tree for the first time.
func _ready():
	print("Ready!");
	print(owner);
	pass # Replace with function body.

func _physics_process(_delta):
	var target_movement: Vector2 = Vector2.ZERO;
	target_movement.x = Input.get_action_strength("move_right") - Input.get_action_strength("move_left");
	if is_on_floor():
		target_movement.y = -Input.get_action_strength("jump");
		velocity.y = target_movement.y * JUMP_POWER;
	if target_movement != Vector2.ZERO:
		velocity = velocity.move_toward(Vector2(target_movement.x * MAX_SPEED, velocity.y), ACCELERATION);
	else:
		velocity = velocity.move_toward(Vector2(0.0, velocity.y), FRICTION)

	# Simulate real gravity; constant force being applied
	velocity.y += GRAVITY;
	velocity = move_and_slide(velocity, Vector2(0.0,-1.0), true);

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
