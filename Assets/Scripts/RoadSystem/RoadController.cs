using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace RoadSystem
{
    public class RoadController : MonoBehaviour
    {
        [SerializeField] private RoadChunk _chunkPrefab;
        [SerializeField] private int _maxChunks = 10;
        [SerializeField] private Transform _startChunkPoint;
        [SerializeField] private float _deconstructRoadDistance;
        
        private ObjectPool<RoadChunk> _chunkPool;
        private readonly Queue<RoadChunk> _roadChunks = new();
        private int _chunkCounter;

        private void Start()
        {
            _chunkPool = new ObjectPool<RoadChunk>(
                createFunc: () => Instantiate(_chunkPrefab, transform),
                actionOnGet: chunk => chunk.gameObject.SetActive(true),
                actionOnRelease: chunk => chunk.gameObject.SetActive(false),
                actionOnDestroy: chunk => Destroy(chunk.gameObject),
                collectionCheck: false);
            
            for (int i = 0; i < _maxChunks; i++)
            {
                SpawnChunk();
            }
        }

        private void Update()
        {
            var lastRoad = _roadChunks.Last();
            var distanceToLastRoad = Vector3.Distance(_startChunkPoint.position, lastRoad.transform.position);
            if (distanceToLastRoad < _deconstructRoadDistance)
            {
                DespawnChunk(lastRoad);
            }
        }

        private void SpawnChunk()
        {
            var newChunk = _chunkPool.Get();
            var lastChunk = _roadChunks.Last();
                
            var offsetZ = lastChunk != null ? lastChunk.HalfLength + newChunk.HalfLength : 0;
            var newRoadPosition = lastChunk != null 
                ? lastChunk.transform.position + new Vector3(0, 0, offsetZ) 
                : _startChunkPoint.position;
            
            newChunk.transform.position = newRoadPosition;
            newChunk.Initialize();
            newChunk.name = "Chunk_" + _chunkCounter;  // just for convenience
            _roadChunks.Enqueue(newChunk);
            _chunkCounter++;
        }

        private void DespawnChunk(RoadChunk chunk)
        {
            _chunkPool.Release(chunk);
        }
    }
}