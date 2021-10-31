use gdnative::api::*;
use gdnative::prelude::*;

#[derive(Debug)]
#[derive(NativeClass)]
#[inherit(KinematicBody2D)]
pub struct Player {
	velocity: Vector2,
}

const ACCELERATION: f32 = 10.0;
const MAX_SPEED: f32 = 100.0;
const FRICTION: f32 = 10.0;

#[methods]
impl Player {
	fn new(_owner: &KinematicBody2D) -> Self {
		godot_print!("Hello from 'new'!");
		Player {
			velocity: Vector2::zero(),
		}
	}

	#[export]
	fn _ready(&mut self, _owner: &KinematicBody2D) {
		godot_print!("Hello from '_ready'!");
	}

	// #[export]
	// fn _process(&mut self, _owner: &KinematicBody2D, _delta:f32) {
	// 	godot_print!("Hello from 'GetInput'!");
		
	// }

	#[export]
	fn _physics_process(&mut self, owner: &KinematicBody2D, delta: f32) {
		godot_print!("Hello from '_physics_process'!");
		let mut target_movement = Vector2::zero();
		let input = Input::godot_singleton();
		target_movement.x = Input::get_action_strength(&input, "move_right") as f32 - Input::get_action_strength(&input, "move_left") as f32;
		target_movement.y = Input::get_action_strength(&input, "move_down") as f32 - Input::get_action_strength(&input, "move_up") as f32;
		if target_movement != Vector2::zero() {
			target_movement = target_movement.normalize();
			self.velocity = self.velocity.move_towards(target_movement * MAX_SPEED * delta, ACCELERATION * delta);
		}
		else {
			self.velocity = self.velocity.move_towards(Vector2::zero(), FRICTION * delta)
		}
		owner.move_and_collide(self.velocity, true, true, false);
		godot_print!("x: {} y: {}",self.velocity.x, self.velocity.y);
	}
}
