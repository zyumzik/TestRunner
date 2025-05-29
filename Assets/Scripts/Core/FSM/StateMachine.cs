using System;
using System.Collections.Generic;

namespace Core.FiniteStateMachine
{
    public class StateMachine
    {
        private StateNode _current;
        private readonly Dictionary<Type, StateNode> _nodes = new();
        private readonly HashSet<ITransition> _anyTransitions = new();
        private readonly HashSet<TimeTrigger> _triggers = new();
        private IState _initialState;

        private readonly bool _allowSelfTransition;

        public event Action<IState> OnStateChanged;

        public StateMachine(bool allowSelfTransition = false)
        {
            _allowSelfTransition = allowSelfTransition;
        }
        
        public void Update()
        {
            foreach (var trigger in _triggers) trigger.Update();

            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.ToState);

            _current.State?.Update();
        }

        public void FixedUpdate()
        {
            _current.State?.FixedUpdate();
        }

        public void LateUpdate()
        {
            _current.State?.LateUpdate();
        }

        public void SetInitialState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.Enter();
            OnStateChanged?.Invoke(state);
            
            _initialState = state;
        }

        public void Reset()
        {
            ChangeState(_initialState);
        }
        
        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        public void AddTimeTrigger(TimeTrigger trigger) => _triggers.Add(trigger);

        private void ChangeState(IState state)
        {
            if (!_allowSelfTransition && state == _current.State) return;

            var prevState = _current.State;
            var nextState = state;

            prevState?.Exit();
            nextState?.Enter();
            _current = _nodes[state.GetType()];

            OnStateChanged?.Invoke(state);
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition.Evaluate())
                    return transition;

            foreach (var transition in _current.Transitions)
                if (transition.Condition.Evaluate())
                    return transition;

            return null;
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}
