using Godot;
namespace Player {
	public class Idle : OnGround {
		public Idle(Player player) {
			this.Player = player;
		}
		public override void Enter() {
			base.Enter();
			GD.Print("Switched to Idle state");
			Player.AnimationState.Travel("Idle");
		}


		public override void Update(float delta) {
			Player.Velocity = Player.Velocity.MoveToward(Vector2.Zero, Player.FRICTION * delta);
		}

		public override void PhysicsUpdate(float delta) {
			Player.Velocity = Player.MoveAndSlideWithSnap(
				Player.Velocity, Player.snapVector, Vector2.Up, true
			);
		}

		public override void UnhandledInput(InputEvent _event) {
			base.UnhandledInput(_event);
		}
	}
}
