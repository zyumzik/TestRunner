using System;
using System.Collections.Generic;
using System.Linq;
using Configurations;
using Core.Managers;
using Core.PoolModule;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace RoadSystem
{
    public class RoadChunkManager
    {
        private readonly RoadChunksConfiguration _roadChunksRoadChunksConfiguration;
        private readonly TicksManager _ticksManager;
        private readonly Transform _startChunkPoint;
        private readonly Transform _clearChunksOnRevivePoint;

        private readonly ShuffledObjectPool<RoadChunk> _chunkPool;
        private readonly Queue<RoadChunk> _roadChunks = new();
        private int _chunkCounter;

        public event Action<RoadChunk> OnChunkSpawned;
        public event Action<RoadChunk> OnChunkDespawned;
        
        public RoadChunkManager(RoadChunksConfiguration roadChunksConfiguration, 
            TicksManager ticksManager, IFactory<Transform, RoadChunk> chunkFactory, 
            Transform startChunkPoint, Transform chunksParent, Transform clearChunksOnRevivePoint)
        {
            _roadChunksRoadChunksConfiguration = roadChunksConfiguration;
            _ticksManager = ticksManager;
            _startChunkPoint = startChunkPoint;
            _clearChunksOnRevivePoint = clearChunksOnRevivePoint;

            _chunkPool = new ShuffledObjectPool<RoadChunk>(
                createFunc: () => chunkFactory.Create(chunksParent),
                actionOnGet: chunk => chunk.gameObject.SetActive(true),
                actionOnRelease: chunk => chunk.gameObject.SetActive(false),
                actionOnDestroy: chunk => Object.Destroy(chunk.gameObject),
                defaultCapacity: _roadChunksRoadChunksConfiguration.InitialPoolSize,
                maxSize: _roadChunksRoadChunksConfiguration.MaxChunks);

            _ticksManager.OnTick += OnTick;
        }

        public void ConstructStartRoad()
        {
            if (_roadChunks.Count > 0) ClearRoad();
            
            for (int i = 0; i < _roadChunksRoadChunksConfiguration.StartChunks; i++)
            {
                SpawnChunk().SetObstaclesActivity(false);
            }

            for (int i = 0; i < _roadChunksRoadChunksConfiguration.MaxChunks - _roadChunks.Count; i++)
            {
                SpawnChunk().SetObstaclesActivity(true);
            }
        }

        public void DeactivateObstaclesOnRevive()
        {
            foreach (var chunk in _roadChunks)
            {
                if (Vector3.Distance(_clearChunksOnRevivePoint.position, 
                    chunk.transform.position) <= _roadChunksRoadChunksConfiguration.ClearRoadsOnReviveDistance)
                {
                    chunk.SetObstaclesActivity(false);
                }
            }
        }

        public void RefillPool()
        {
            _chunkPool.Refill();
        }
        
        private void OnTick()
        {
            if (_roadChunks.Count == 0) return;
            
            var peekRoad = _roadChunks.Peek();
            var distanceToPeekRoad = Vector3.Distance(_startChunkPoint.position, peekRoad.transform.position);
            if (distanceToPeekRoad <= _roadChunksRoadChunksConfiguration.DeconstructRoadDistance)
            {
                DespawnChunk();
                for (int i = 0; i < _roadChunksRoadChunksConfiguration.MaxChunks - _roadChunks.Count; i++)
                {
                    SpawnChunk().SetObstaclesActivity(true);
                }
            }
        }

        private void ClearRoad()
        {
            while (_roadChunks.Count > 0)
            {
                DespawnChunk();
            }
        }
        
        private RoadChunk SpawnChunk()
        {
            var newChunk = _chunkPool.Get();
            
            if (_roadChunks.Count > 0)
            {
                var lastChunk = _roadChunks.Last();
                var offsetZ = lastChunk.HalfLength + newChunk.HalfLength;
                newChunk.transform.position = lastChunk.transform.position + new Vector3(0, 0, offsetZ);
            }
            else
            {
                newChunk.transform.position = _startChunkPoint.position + new Vector3(0, 0, newChunk.HalfLength);
            }
            
            newChunk.name = "Chunk_" + _chunkCounter;  // just for convenience
            _roadChunks.Enqueue(newChunk);
            _chunkCounter++;

            OnChunkSpawned?.Invoke(newChunk);
            return newChunk;
        }

        private void DespawnChunk()
        {
            var chunk = _roadChunks.Dequeue();
            _chunkPool.Release(chunk);
            OnChunkDespawned?.Invoke(chunk);
        }
    }
}