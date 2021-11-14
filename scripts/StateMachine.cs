using Godot;
using System;
using System.Collections.Generic;

// Generic state machine. Initializes states and delegates engine callbacks
// (_physics_process, _unhandled_input) to the active state.
public class StateMachine {
    protected Player player;
    public StateMachine(Player player) {
        this.player = player;
    }

    // The current active state. This is undefined until add_states is called
    public IState currentState { get; private set; }
    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> currentTransitions = new List<Transition>();
    private List<Transition> anyTransitions = new List<Transition>();
    private List<Transition> emptyTransitions = new List<Transition>(0);

    public void AddTransition(IState to, IState from, Func<bool> predicate) {
        Transition transition = new Transition(to, predicate);

        if (!transitions.ContainsKey(from.GetType())) {
            List<Transition> _transitions = new List<Transition>();
            transitions[from.GetType()] = _transitions;
        }

        transitions[from.GetType()].Add(transition);
    }

    public void AddAnyTransition(IState state, Func<bool> predicate) {
        anyTransitions.Add(new Transition(state, predicate));
    }


    // The state machine subscribes to node callbacks and delegates them to the state objects.
    public void UnhandledInput(InputEvent _event) {
        currentState?.UnhandledInput(_event);
    }


    public void Process(float delta) {
        var transition = GetTransition();

        if (transition != null) {
            SetState(transition.to);
        }
        currentState?.Update(delta);
    }


    public void PhysicsProcess(float delta) {
        currentState?.PhysicsUpdate(delta);
    }


    // This function calls the current state's exit() function, then changes the active state,
    // and calls its enter function.
    // It optionally takes a `msg` dictionary to pass to the next state's enter() function.
    public void SetState(IState targetState) {
        if (targetState == currentState) {
            return;
        }

        currentState?.Exit();

        currentState = targetState;

        currentTransitions = transitions.ContainsKey(targetState.GetType()) ? transitions[targetState.GetType()] : emptyTransitions;

        currentState?.Enter();
    }


    class Transition {
        public IState to;
        public Func<bool> condition;

        public Transition(IState _to, Func<bool> _condition) {
            to = _to;
            condition = _condition;
        }
    }


    private Transition GetTransition() {
        foreach (Transition transition in anyTransitions) {
            if (transition.condition()) {
                return transition;
            }
        }

        foreach (Transition transition in currentTransitions) {
            if (transition.condition()) {
                return transition;
            }
        }
        return null;
    }


}
