class_name PlayerState
extends State

var player = null;

func _ready():
	yield(owner, "ready");
	player = owner
	assert(player != null);
	print("once")

func _process(delta):
#	print("im running in playerstate")
	pass
	
