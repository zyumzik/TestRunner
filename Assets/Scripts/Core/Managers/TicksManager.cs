using System;
using Zenject;

namespace Core.Managers
{
    public class TicksManager : ITickable, IFixedTickable, ILateTickable
    {
        public event Action OnTick;
        public event Action OnFixedTick;
        public event Action OnLateTick;
        
        public void Tick()
        {
            OnTick?.Invoke();
        }

        public void FixedTick()
        {
            OnFixedTick?.Invoke();
        }

        public void LateTick()
        {
            OnLateTick?.Invoke();
        }
    }

}