using System;
using Core.Managers;
using UnityEngine;

namespace Core.TimerModule
{
    public class Timer : IDisposable
    {
        public bool IsRunning { get; private set; }
        public float Duration { get; private set; }
        public float ElapsedTime { get; private set; }
        public float Progress01 => Mathf.Clamp01(ElapsedTime / Duration);
        
        private readonly TicksManager _ticksManager;

        public event Action OnCompleted;

        public Timer(TicksManager ticksManager)
        {
            _ticksManager = ticksManager;
        }

        public void Start(float duration)
        {
            IsRunning = true;
            Duration = duration;
            ElapsedTime = 0;
            _ticksManager.OnTick += OnTick;
        }

        public void Stop()
        {
            IsRunning = false;
            _ticksManager.OnTick -= OnTick;
        }

        private void OnTick()
        {
            ElapsedTime += Time.deltaTime;
            if (ElapsedTime >= Duration)
            {
                ElapsedTime = Duration;
                Stop();
                OnCompleted?.Invoke();
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}