using System;

namespace Core.FiniteStateMachine
{
    public class Condition : IPredicate
    {
        private readonly Func<bool> _func;

        public Condition(Func<bool> func) { _func = func; }

        public bool Evaluate() => _func.Invoke();
    }
}
