using UnityEngine;

namespace Core.FiniteStateMachine
{
    public class TimeTrigger : IPredicate
    {
        private bool _value;
        private readonly float _triggerDuration;
        private float _timeElapsed;

        public TimeTrigger(StateMachine stateMachine, float triggerDuration)
        {
            _triggerDuration = triggerDuration;
            stateMachine.AddTimeTrigger(this);
        }

        public bool Evaluate()
        {
            return _value;
        }
        
        public void Update()
        {
            if (!_value) return;

            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _triggerDuration)
            {
                _timeElapsed = 0;
                _value = false;
            }
        }

        public void Activate() => _value = true;

        public static implicit operator bool(TimeTrigger trigger) => trigger._value;
        public static bool operator !(TimeTrigger trigger) => !trigger._value;
    }
}
