using System;
using UnityEngine;

namespace RoadSystem.Obstacle
{
    public class RoadObstacle : MonoBehaviour
    {
        [SerializeField] private GameObject _model;
        [SerializeField] private Collider[] _colliders;
        
        public event Action OnRestored;
        public event Action OnDestroyed;
        
        public void RestoreObstacle()
        {
            _model.SetActive(true);
            foreach(var collider in _colliders) collider.enabled = true;
            
            OnRestored?.Invoke();
        }

        public void DestroyObstacle()
        {
            _model.SetActive(false);
            foreach(var collider in _colliders) collider.enabled = false;
            
            OnDestroyed?.Invoke();
        }
    }
}