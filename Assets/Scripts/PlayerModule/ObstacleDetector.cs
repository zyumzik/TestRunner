using System;
using RoadSystem.Obstacle;
using UnityEngine;

namespace PlayerModule
{
    public class ObstacleDetector : MonoBehaviour
    {
        public event Action<RoadObstacle> OnObstacleEnter;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<RoadObstacle>(out var obstacle))
            {
                OnObstacleEnter?.Invoke(obstacle);
            }
        }
    }
}