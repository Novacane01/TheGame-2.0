extends Node

onready var state_machine: StateMachine = $StateMachine
var player = null


func _ready() -> void:
	yield(owner, "ready")
	player = owner
	assert(player != null)

	var AIR_STATE = load("res://scripts/Player/States/Airborne.gd").new(player)
	var IDLE_STATE = load("res://scripts/Player/States/Idle.gd").new(player)
	var RUN_STATE = load("res://scripts/Player/States/Run.gd").new(player)
	var ATTACK_STATE = load("res://scripts/Player/States/Attack.gd").new(player)
	var JUMP_ATTACK_STATE = load("res://scripts/Player/States/JumpAttack.gd").new(player)

	state_machine.add_states([AIR_STATE, IDLE_STATE, RUN_STATE, ATTACK_STATE, JUMP_ATTACK_STATE])

	state_machine.add_transition(RUN_STATE, AIR_STATE, funcref(self, "isAirborne"))
	state_machine.add_transition(IDLE_STATE, AIR_STATE, funcref(self, "isAirborne"))

	state_machine.add_transition(ATTACK_STATE, RUN_STATE, funcref(self, "isRunning"))
	state_machine.add_transition(JUMP_ATTACK_STATE, RUN_STATE, funcref(self, "isRunning"))
	state_machine.add_transition(IDLE_STATE, RUN_STATE, funcref(self, "isRunning"))

	state_machine.add_any_transition(IDLE_STATE, funcref(self, "isIdle"))

	state_machine.add_transition(AIR_STATE, JUMP_ATTACK_STATE, funcref(self, "isAttacking"))

	state_machine.add_transition(IDLE_STATE, ATTACK_STATE, funcref(self, "isAttacking"))
	state_machine.add_transition(RUN_STATE, ATTACK_STATE, funcref(self, "isAttacking"))


func isAirborne() -> bool:
	return !player.is_on_floor()


func isRunning() -> bool:
	return player.get_input_direction().x != 0.0 && !isAttacking()


func isIdle() -> bool:
	return !isAirborne() && !isRunning() && !isAttacking()


func isAttacking() -> bool:
	return Input.is_action_just_pressed("attack") || player.isAttacking
