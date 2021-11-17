using Godot;
using System;

namespace Enemy {
    public class WanderController: Node2D {
        public Vector2 InitialPosition;
		public Vector2 TargetPosition;
        private Timer _timer = null;
        public override void _Ready() {
            InitialPosition = GlobalPosition;
            TargetPosition = GlobalPosition;
            _timer = GetNode<Timer>("Timer");
            _timer.OneShot = true;
        }

        public void StartTimer(float duration = -1) {
            _timer.Start(duration);
        }
        public void SetTime(float value) {
            _timer.WaitTime = value;
        }

        public float GetTimeLeft() {
            return _timer.TimeLeft;
        }
    }
}
