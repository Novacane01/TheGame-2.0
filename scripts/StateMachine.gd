# Generic state machine. Initializes states and delegates engine callbacks
# (_physics_process, _unhandled_input) to the active state.
class_name StateMachine
extends Node

# Path to the initial active state. We export it to be able to pick the initial state in the inspector.
export var initial_state := NodePath()

# The current active state. At the start of the game, we get the `initial_state`.
onready var current_state: State = get_node(initial_state)

var transitions: Dictionary = {};
var current_transitions: Array;
var any_transitions: Array;
var empty_transitions: Array = [];
onready var children: Array = get_children();

func _ready() -> void:
	yield(owner, "ready")
	# The state machine assigns itself to the State objects' state_machine property.
	for child in children:
		child.state_machine = self
	current_state.enter()

func add_transition(from: Object, to: Object, predicate: FuncRef):
	if (!transitions.has(from)):
		var _transitions: Array = [];
		_transitions.push_back(Transition.new(to, predicate, children));
		transitions[from.to_string()] = _transitions;
	else:
		transitions[from.to_string()].push_back(Transition.new(to, predicate, children));
		
func add_any_transition(state: Object, predicate: FuncRef):
	any_transitions.push_back(Transition.new(state, predicate, children));
		
# The state machine subscribes to node callbacks and delegates them to the state objects.
func _unhandled_input(event: InputEvent) -> void:
	current_state.handle_input(event)

func _process(delta: float) -> void:
	var transition = get_transition();
	if (transition != null and current_state.to_string() != transition.to.to_string()): # Refactor later this is hideous
		transition_to(transition.to.to_string())
	current_state.update(delta)


func _physics_process(delta: float) -> void:
	current_state.physics_update(delta)

# This function calls the current state's exit() function, then changes the active state,
# and calls its enter function.
# It optionally takes a `msg` dictionary to pass to the next state's enter() function.
func transition_to(target_state_name: String, msg: Dictionary = {}) -> void:
	# Safety check, you could use an assert() here to report an error if the state name is incorrect.
	# We don't use an assert here to help with code reuse. If you reuse a state in different state machines
	# but you don't want them all, they won't be able to transition to states that aren't in the scene tree.
	if (target_state_name != current_state.to_string() || not has_node(target_state_name)):
		return
	if (current_state != null):
		current_state.exit()
	current_state = get_node(target_state_name)
	if (target_state_name in transitions):
		current_transitions = transitions[target_state_name];
	else:
		current_transitions = empty_transitions;
	current_state.enter(msg)


class Transition:
	var to: Object;
	var condition: FuncRef;

	func _init(_to: Object, _condition: FuncRef, state_table: Array):
		if !(_to in state_table):
			return printerr("Provided state is not present in Child heirarchy.");
		to = _to;
		condition = _condition;

func get_transition() -> Transition:
	for transition in any_transitions:
		if (transition.condition.call_func()):
			return transition
	for transition in current_transitions:
		if (transition.condition.call_func()):
			return transition
	return null;
