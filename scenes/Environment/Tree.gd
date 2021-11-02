extends StaticBody2D

onready var stats = $Stats;

func _on_HurtBox_area_entered(area):
	stats.health -= 10.0;
	if stats.health <= 0.0:
		queue_free();
	
