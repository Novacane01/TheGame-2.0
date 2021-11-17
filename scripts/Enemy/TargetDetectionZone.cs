using Godot;
using System;

namespace Enemy {
	public class TargetDetectionZone : Area2D {
		public object target = null;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready() {
			
		}

		public bool canSeeTarget() {
			return target != null;
		}
		private void _on_TargetDetectionZone_body_entered(object body) {
			target = body;
		}

		private void _on_TargetDetectionZone_body_exited(object body) {
			target = null;
		}
	}
}
