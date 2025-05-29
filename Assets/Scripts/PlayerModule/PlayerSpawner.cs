using UnityEngine;
using Zenject;

namespace PlayerModule
{
    public class PlayerSpawner
    {
        private readonly DiContainer _container;
        private readonly Player _playerPrefab;
        private readonly Transform _playerParent;
        private readonly Transform _spawnPoint;

        private Player _player;
        
        public PlayerSpawner(DiContainer container, Player playerPrefab, Transform playerParent, Transform spawnPoint)
        {
            _container = container;
            _playerPrefab = playerPrefab;
            _playerParent = playerParent;
            _spawnPoint = spawnPoint;
        }

        public Player Spawn()
        {
            if (_player != null) return _player;
            
            _player = _container.InstantiatePrefabForComponent<Player>(_playerPrefab, _spawnPoint.position,
                _spawnPoint.rotation, _playerParent);
            return _player;
        }
    }
}