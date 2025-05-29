using System;
using Configurations;
using Core.Managers;
using UnityEngine;

namespace RoadSystem
{
    public class RoadScoreCounter
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly TicksManager _ticksManager;

        public int TimeScore { get; private set; }
        
        private bool _isActive;
        private float _elapsedTime;

        public event Action<int> OnTimeScoreChanged; 
        
        public RoadScoreCounter(GameConfiguration gameConfiguration, TicksManager ticksManager)
        {
            _gameConfiguration = gameConfiguration;
            _ticksManager = ticksManager;

            _ticksManager.OnTick += OnTick;
        }

        public void Start()
        {
            _isActive = true;   
        }

        public void Stop()
        {
            _isActive = false;
        }
        
        public void Reset()
        {
            _elapsedTime = 0f;
            TimeScore = 0;
        }
        
        private void OnTick()
        {
            if (!_isActive) return;
                
            _elapsedTime += Time.deltaTime;

            TimeScore = (int)(_elapsedTime * _gameConfiguration.ScorePerSecond);
            OnTimeScoreChanged?.Invoke(TimeScore);
        }
    }
}