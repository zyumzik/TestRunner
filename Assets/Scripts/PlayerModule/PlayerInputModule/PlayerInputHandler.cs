using System;
using Core.Managers;
using UnityEngine;

namespace PlayerModule.PlayerInputModule
{
    public class PlayerInputHandler
    {
        private readonly TicksManager _ticksManager;
        private readonly PlayerInput _playerInput;

        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnJump;
        public event Action OnSlide;
        
        public PlayerInputHandler(TicksManager ticksManager, PlayerInput playerInput)
        {
            _ticksManager = ticksManager;
            _playerInput = playerInput;

            _ticksManager.OnTick += OnTick;
        }

        private void OnTick()
        {
            var input = _playerInput.GetDirection();
            if (input == Vector2.zero) return;

            if (input.x < 0)
            {
                OnMoveLeft?.Invoke();
            }
            else if (input.x > 0)
            {
                OnMoveRight?.Invoke();
            }
            else if (input.y > 0)
            {
                OnJump?.Invoke();
            }
            else if (input.y < 0)
            {
                OnSlide?.Invoke();
            }
        }
    }
}