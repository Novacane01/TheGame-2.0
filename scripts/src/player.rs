use gdnative::api::*;
use gdnative::prelude::*;

#[derive(Debug)]
#[derive(NativeClass)]
#[inherit(KinematicBody2D)]
pub struct Player {
	velocity: Vector2,
	on_ground: bool
}

const ACCELERATION: f32 = 10.0;
const MAX_SPEED: f32 = 100.0;
const FRICTION: f32 = 10.0;
const GRAVITY: f32 = 5.0;
const JUMP_POWER: f32 = 100.0;

#[methods]
impl Player {
	fn new(_owner: &KinematicBody2D) -> Self {
		godot_print!("Hello from 'new'!");
		Player {
			velocity: Vector2::zero(),
			on_ground: false
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
		// Only allow player to jump if they are on the ground
		if owner.is_on_floor() {
			target_movement.y = -Input::get_action_strength(&input, "jump") as f32;
			self.velocity.y = target_movement.y * JUMP_POWER;
		}
		if target_movement != Vector2::zero() {
			self.velocity = self.velocity.move_towards(Vector2::new(target_movement.x * MAX_SPEED, self.velocity.y), ACCELERATION);
		}
		else {
			self.velocity = self.velocity.move_towards(Vector2::new(0.0, self.velocity.y), FRICTION)
		}
		// Simulate real gravity; constant force being applied
		self.velocity.y += GRAVITY;
		self.velocity = owner.move_and_slide(self.velocity, Vector2::new(0.0,-1.0), false, 4, 0.785398, true);
		godot_print!("x: {} y: {}",self.velocity.x, self.velocity.y);
	}
}
