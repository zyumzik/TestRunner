using UnityEngine;

namespace RoadSystem.Obstacle
{
    public class RoadObstacleVisuals :  MonoBehaviour
    {
        [SerializeField] private RoadObstacle _roadObstacle;
        [SerializeField] private ParticleSystem[] _destructionParticles;
        
        private void OnEnable()
        {
            _roadObstacle.OnRestored += OnRestored;
            _roadObstacle.OnDestroyed += OnDestroyed;
        }

        private void OnDisable()
        {
            _roadObstacle.OnRestored -= OnRestored;
            _roadObstacle.OnDestroyed -= OnDestroyed;
        }

        private void OnRestored()
        {
            foreach (var particle in _destructionParticles)
            {
                particle.Stop();
            }
        }

        private void OnDestroyed()
        {
            foreach (var particle in _destructionParticles)
            {
                particle.Play();
            }
        }
    }
}