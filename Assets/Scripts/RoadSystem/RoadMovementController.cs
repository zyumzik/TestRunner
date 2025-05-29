using System.Collections.Generic;
using Configurations;
using Core.Managers;
using UnityEngine;

namespace RoadSystem
{
    public class RoadMovementController
    {
        public float CurrentSpeedRatio => Mathf.Clamp01((_currentSpeed - _roadMovementConfiguration.MinSpeed) /
                                                        (_roadMovementConfiguration.MaxSpeed -
                                                         _roadMovementConfiguration.MinSpeed));
        
        private readonly RoadMovementConfiguration _roadMovementConfiguration;
        private readonly TicksManager _ticksManager;
        private readonly RoadChunkManager _roadChunkManager;

        private readonly HashSet<RoadChunk> _currentChunks = new();
        private bool _isActive;
        private float _currentSpeed;
        private float _elapsedTime;
        
        public RoadMovementController(RoadMovementConfiguration roadMovementConfiguration, TicksManager ticksManager, 
            RoadChunkManager roadChunkManager)
        {
            _roadMovementConfiguration = roadMovementConfiguration;
            _ticksManager = ticksManager;
            _roadChunkManager = roadChunkManager;

            _roadChunkManager.OnChunkSpawned += OnChunkSpawned;
            _roadChunkManager.OnChunkDespawned += OnChunkDespawned;
            _ticksManager.OnTick += OnTick;
        }

        public void StartMoving()
        {
            _isActive = true;
        }

        public void StopMoving()
        {
            _isActive = false;
        }

        public void Reset()
        {
            _elapsedTime = 0;
            _currentSpeed = 0;
        }

        private void OnChunkSpawned(RoadChunk chunk)
        {
            _currentChunks.Add(chunk);
        }
        
        private void OnChunkDespawned(RoadChunk chunk)
        {
            _currentChunks.Remove(chunk);
        }
        
        private void OnTick()
        {
            if (!_isActive) return;
            
            _elapsedTime += Time.deltaTime;

            var t = Mathf.Clamp01(_elapsedTime / _roadMovementConfiguration.SpeedUpDuration);
            var curveValue = _roadMovementConfiguration.SpeedCurve.Evaluate(t);
            _currentSpeed = Mathf.Lerp(_roadMovementConfiguration.MinSpeed, _roadMovementConfiguration.MaxSpeed,
                curveValue);

            foreach (var chunk in _currentChunks)
            {
                var translation = _roadMovementConfiguration.MovementDirection * (_currentSpeed * Time.deltaTime);
                chunk.transform.Translate(translation);
            }
            
            //Debug.Log($"Current speed: {_currentSpeed}. 01: {CurrentSpeedRatio}");
        }
    }
}