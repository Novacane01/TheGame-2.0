using Godot;

namespace Player {
    public class PlayerHealth : Health {

        private GameEvents gameEvents;

        public override void _Ready() {
            base._Ready();
            gameEvents = GetNode<GameEvents>("/root/GameEvents");
            gameEvents.EmitPlayerHealthChanged(this);
        }

        public override double Current {
            get { return health; }
            set {
                health = value;
                gameEvents.EmitPlayerHealthChanged(this);

                if (health <= 0) {
                    EmitNoHealthSignal();
                }

            }
        }
    }
}
