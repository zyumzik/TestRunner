using Configurations;
using DG.Tweening;
using PlayerModule.PlayerInputModule;
using RoadSystem;
using UnityEngine;
using Zenject;

namespace PlayerModule
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        [Inject] private PlayerConfiguration _playerConfiguration;
        [Inject] private PlayerInputHandler _playerInputHandler;
        [Inject] private RoadLaneManager _roadLaneManager;

        private Tween _changeLaneTween;

        private void OnEnable()
        {
            _playerInputHandler.OnMoveLeft += OnMoveLeft;
            _playerInputHandler.OnMoveRight += OnMoveRight;
            _playerInputHandler.OnJump += OnJump;
            _playerInputHandler.OnSlide += OnSlide;
        }

        private void OnDisable()
        {
            _playerInputHandler.OnMoveLeft -= OnMoveLeft;
            _playerInputHandler.OnMoveRight -= OnMoveRight;
            _playerInputHandler.OnJump -= OnJump;
            _playerInputHandler.OnSlide -= OnSlide;
        }

        private void OnMoveLeft()
        {
            if (_roadLaneManager.TryChangeLane(RoadLane.Direction.Left, out var newLane))
            {
                MoveToLane(newLane);
            }
        }

        private void OnMoveRight()
        {
            if (_roadLaneManager.TryChangeLane(RoadLane.Direction.Right, out var newLane))
            {
                MoveToLane(newLane);
            }
        }

        private void OnJump()
        {
            _player.Jump();
        }

        private void OnSlide()
        {
            _player.Slide();
        }
        
        private void MoveToLane(RoadLane newLane)
        {
            if (_changeLaneTween != null && _changeLaneTween.IsActive())
                _changeLaneTween.Kill();
                
            _changeLaneTween = 
                _player.transform.DOMove(newLane.LaneTransform.position, _playerConfiguration.ChangeLaneDuration)
                    .SetEase(_playerConfiguration.ChangeLaneEase);
        }
    }
}