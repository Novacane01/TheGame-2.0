class_name Player
extends KinematicBody2D

export var ACCELERATION: float = 800.0;
export var MAX_SPEED: float = 100.0;
export var FRICTION: float = 800.0;
export var GRAVITY: float = 600.0;
export var JUMP_POWER: float = 150.0;

var velocity: Vector2 = Vector2.ZERO;

onready var sprite: Sprite = $Sprite;
onready var state_machine = $StateMachine;
onready var AIR_STATE = $StateMachine/Air;
onready var IDLE_STATE = $StateMachine/Idle;
onready var RUN_STATE = $StateMachine/Run;
	
func _ready() -> void:
	print("Player ready!");

	state_machine.add_any_transition(AIR_STATE, funcref(self, "isAirbourne"));
	state_machine.add_transition(AIR_STATE, RUN_STATE, funcref(self, "isRunning"));
	state_machine.add_transition(IDLE_STATE, RUN_STATE, funcref(self, "isRunning"));
	state_machine.add_any_transition(IDLE_STATE, funcref(self, "isIdle"));
	
func get_input_direction() -> Vector2:
	return Vector2((
		Input.get_action_strength("move_right")
		- Input.get_action_strength("move_left")
	),
	Input.get_action_strength("jump"));

func isAirbourne() -> bool:
	return !is_on_floor();
	
func isRunning() -> bool:
	return get_input_direction().x != 0.0;

func isIdle() -> bool:
	return !isAirbourne() && !isRunning();
	

