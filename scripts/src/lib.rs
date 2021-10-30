use gdnative::api::KinematicBody2D;
use gdnative::api::Input;
use gdnative::prelude::*;

#[derive(Debug)]
#[derive(NativeClass)]
#[inherit(KinematicBody2D)]
struct Movement {
	velocity: Vector2,
	speed: f32,
	target_movement: Vector2
}
#[gdnative::methods]
impl Movement {
	fn new(_owner: &KinematicBody2D) -> Self {
		godot_print!("Hello from 'new'!");
		Movement {
			velocity: Vector2::zero(),
			speed: 100.0,
			target_movement: Vector2::zero()
		}
	}

	#[export]
	fn _ready(&mut self, _owner: &KinematicBody2D) {
		godot_print!("Hello from '_ready'!");
		godot_print!("{:?}", self);
		self.velocity = Vector2::zero();
	}

	#[export]
	fn _process(&mut self, _owner: &KinematicBody2D, _delta:f32) {
		godot_print!("Hello from 'GetInput'!");
		self.target_movement = Vector2::zero();

		let input = Input::godot_singleton();
		if Input::is_action_pressed(input, "move_right") {
			self.target_movement.x += 1.0;
		}
		if Input::is_action_pressed(input, "move_left") {
			self.target_movement.x -= 1.0;
		}
		if Input::is_action_pressed(input, "move_up") {
			self.target_movement.y += 1.0;
		}
		if Input::is_action_pressed(input, "move_down") {
			self.target_movement.y -= 1.0;
		}
		// if InputEvent::is_action_released(,"") {

		// }
		godot_print!("{:?}", self);
	}

	#[export]
	fn _physics_process(&mut self, owner: &KinematicBody2D, _delta: f32) {
		self.target_movement *= self.speed;
		self.velocity = self.target_movement;
		godot_print!("Hello from '_physics_process'!");
		self.velocity = owner.move_and_slide(self.velocity, Vector2::zero(), false, 4, 0.7, true);
		godot_print!("x: {} y: {}",self.velocity.x, self.velocity.y);
		// self.velocity = KinematicBody2D::move_and_slide(&self, velocity));
	}
}

fn init(handle: InitHandle) {
	handle.add_class::<Movement>();
}

godot_init!(init);
