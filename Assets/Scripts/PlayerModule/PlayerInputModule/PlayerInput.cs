using UnityEngine;

namespace PlayerModule.PlayerInputModule
{
    public class PlayerInput
    {
        private readonly IPlayerInputStrategy _playerInputStrategy;

        public PlayerInput(IPlayerInputStrategy playerInputStrategy)
        {
            _playerInputStrategy = playerInputStrategy;
        }

        public Vector2 GetDirection() => _playerInputStrategy.GetInputDirection();
    }
}