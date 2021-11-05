# Generic state machine. Initializes states and delegates engine callbacks
# (_physics_process, _unhandled_input) to the active state.
class_name StateMachine
extends Node

# Path to the initial active state. We export it to be able to pick the initial state in the inspector.
var initial_index: int = 0

# The current active state. This is undefined until add_states is called
var current_state: State
export var _states: Array = []

var transitions: Dictionary = {}
var current_transitions: Array
var any_transitions: Array


func add_states(states: Array, starting_index: int = 0) -> void:
	if states.empty() || starting_index >= states.size():
		return

	_states = states
	var id = 0
	for state in _states:
		state.id = id
		# The state machine assigns itself to the State objects' state_machine property.
		id += 1

	current_state = _states[starting_index]


#func _ready() -> void:
#	yield(owner, "ready")


func add_transition(from: State, to: State, predicate: FuncRef):
	if !transitions.has(from.id):
		var _transitions: Array = []
		_transitions.push_back(Transition.new(to, predicate, _states))
		transitions[from.id] = _transitions
	else:
		transitions[from.id].push_back(Transition.new(to, predicate, _states))


func add_any_transition(state: State, predicate: FuncRef):
	any_transitions.push_back(Transition.new(state, predicate, _states))


# The state machine subscribes to node callbacks and delegates them to the state objects.
func _unhandled_input(event: InputEvent) -> void:
	current_state.handle_input(event)


func _process(delta: float) -> void:
	var transition = get_transition()

	if transition != null and current_state.id != transition.to.id:  # Refactor later this is hideous
		transition_to(transition.to.id)
	current_state.update(delta)


func _physics_process(delta: float) -> void:
	current_state.physics_update(delta)


# This function calls the current state's exit() function, then changes the active state,
# and calls its enter function.
# It optionally takes a `msg` dictionary to pass to the next state's enter() function.
func transition_to(target_state_id: int, msg: Dictionary = {}) -> void:
	# Safety check, you could use an assert() here to report an error if the state name is incorrect.
	# We don't use an assert here to help with code reuse. If you reuse a state in different state machines
	# but you don't want them all, they won't be able to transition to states that aren't in the scene tree.
	if (
		target_state_id == current_state.id
		|| target_state_id >= _states.size()
		|| _states[target_state_id] == null
	):
		return

	current_state.exit()

	current_state = _states[target_state_id]

	current_transitions = transitions.get(target_state_id, [])

	current_state.enter(msg)


class Transition:
	var to: State
	var condition: FuncRef

	func _init(_to: State, _condition: FuncRef, state_table: Array):
		if !(_to in state_table):
			return printerr("Provided state is not present in Child heirarchy.")
		to = _to
		condition = _condition


func get_transition() -> Transition:
	for transition in any_transitions:
		#print("transition: ", transition.to.to_string())
		if transition.condition.call_func():
			return transition
	#print("current transitions: ", current_transitions.size())
	for transition in current_transitions:
		if transition.condition.call_func():
			#print("transitioning to: ", transition.to_string())
			return transition
	return null

#func force_transition(index: int) -> void:
