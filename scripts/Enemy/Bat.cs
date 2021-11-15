using Godot;
using System;

namespace Enemy {
	public class Bat : Enemy {
		private TargetDetectionZone _targetDetectionZone = null;
		public override void _Ready() {
			base._Ready();
			_targetDetectionZone = GetNode("TargetDetectionZone") as TargetDetectionZone;
			WanderController = GetNode("WanderController") as WanderController;
			var wander = new Wander(this);
			var chase = new Chase(this);
			var idle = new Idle(this);
			Func<bool> hasTarget = () => {
				if (_targetDetectionZone.canSeeTarget()) {
					Target = _targetDetectionZone.target;
					return true;
				}
				return false;
			};
			Func<bool> isIdle = () => Mathf.Abs(GlobalPosition.DistanceTo(WanderController.TargetPosition)) <= MAX_SPEED * GetProcessDeltaTime();
			Func<bool> shouldWander = () => WanderController.GetTimeLeft() == 0.0f;
			StateMachine.AddTransition(wander, idle, shouldWander);
			StateMachine.AddTransition(idle, chase, () => !hasTarget());
			StateMachine.AddTransition(idle, wander, isIdle);
			StateMachine.AddAnyTransition(chase, hasTarget);
			StateMachine.SetState(idle);
		}
	}
}
