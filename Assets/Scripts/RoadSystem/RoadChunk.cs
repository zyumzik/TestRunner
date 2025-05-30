using Configurations;
using RoadSystem.Obstacle;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace RoadSystem
{
    public class RoadChunk : MonoBehaviour
    {
        [SerializeField] private BoxCollider _floorCollider;
        [SerializeField] private RoadObstacle[] _obstacles;
        public float HalfLength => _floorCollider.size.z / 2;
        
        public void SetObstaclesActivity(bool value)
        {
            foreach (var obstacle in _obstacles)
            {
                obstacle.gameObject.SetActive(value);
                obstacle.RestoreObstacle();
            }
        }
        
        public class RandomChunkFactory : IFactory<Transform, RoadChunk>
        {
            private readonly DiContainer _container;
            private readonly RoadChunksConfiguration _configuration;

            public RandomChunkFactory(DiContainer container, RoadChunksConfiguration configuration)
            {
                _container = container;
                _configuration = configuration;
            }

            public RoadChunk Create(Transform parent)
            {
                var randomChunkIndex = Random.Range(0, _configuration.RoadChunkPrefabs.Length);
                var chunkPrefab = _configuration.RoadChunkPrefabs[randomChunkIndex];
                var chunk = _container.InstantiatePrefabForComponent<RoadChunk>(chunkPrefab, parent);
                return chunk;
            }
        }
    }
}