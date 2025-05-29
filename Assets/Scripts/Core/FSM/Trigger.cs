namespace Core.FiniteStateMachine
{
    public class Trigger : IPredicate
    {
        private bool _value;

        public Trigger(bool startValue = false)
        {
            _value = startValue;
        }
        
        public bool Evaluate()
        {
            if (_value)
            {
                _value = false;
                return true;
            }
            return false;
        }

        public void Activate() => _value = true;
        
        public static implicit operator bool(Trigger trigger) => trigger._value;
        public static bool operator !(Trigger trigger) => !trigger._value;
    }
}