using UnityEngine;

namespace PlayerModule.PlayerInputModule
{
    public interface IPlayerInputStrategy
    {
        Vector2 GetInputDirection();
    }
}