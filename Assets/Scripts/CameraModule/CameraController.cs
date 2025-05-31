using Cinemachine;
using DG.Tweening;
using GameStateManagerModule;
using UnityEngine;
using Zenject;

namespace CameraModule
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _initialCamera;
        [SerializeField] private CinemachineVirtualCamera _inGameCamera;
        [SerializeField] private CinemachineVirtualCamera _deathCamera;
        
        [Inject] private GameStateManager _gameStateManager;
        
        private Sequence _cameraTween;

        private void OnEnable()
        {
            _gameStateManager.OnGamePrepared += OnGamePrepared;
            _gameStateManager.OnGameStart += OnGameStart;
            _gameStateManager.OnGameEnd += OnGameEnd;
            _gameStateManager.OnGameRestarted += OnGameRestarted;
        }

        private void OnDisable()
        {
            _gameStateManager.OnGamePrepared -= OnGamePrepared;
            _gameStateManager.OnGameStart -= OnGameStart;
            _gameStateManager.OnGameEnd -= OnGameEnd;
            _gameStateManager.OnGameRestarted -= OnGameRestarted;
        }

        private void OnGamePrepared()
        {
            _initialCamera.enabled = true;
            _inGameCamera.enabled = _deathCamera.enabled = false;
        }

        private void OnGameStart()
        {
            _inGameCamera.enabled = true;
            _initialCamera.enabled = _deathCamera.enabled = false;
        }

        private void OnGameEnd()
        {
            _deathCamera.enabled = true;
            _initialCamera.enabled = _inGameCamera.enabled = false;
        }

        private void OnGameRestarted()
        {
            _inGameCamera.enabled = true;
            _initialCamera.enabled = _deathCamera.enabled = false;
        }
    }
}