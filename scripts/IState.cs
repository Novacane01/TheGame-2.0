using Godot;
using System;

    public abstract class IState
    {
        public virtual void UnhandledInput(InputEvent _event) {}

        // Virtual function. Corresponds to the `_process()` callback.
        public virtual void Update(float delta) {}


        // Virtual function. Corresponds to the `_physics_process()` callback.
        public virtual void PhysicsUpdate(float delta) {}


        // Virtual function. Called by the state machine upon changing the active state. The `msg` parameter
        // is a dictionary with arbitrary data the state can use to initialize itself.
        public virtual void Enter() {}


        // Virtual function. Called by the state machine before changing the active state. Use this function
        // to clean up the state.
        public virtual void Exit() {}
    }
