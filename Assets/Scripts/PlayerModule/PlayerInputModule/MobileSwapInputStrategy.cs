using System;
using UnityEngine;

namespace PlayerModule.PlayerInputModule
{
    public class MobileSwapInputStrategy : IPlayerInputStrategy
    {
        private Vector2 _start;
        private Vector2 _end;
        private Vector2 _direction;
        
        public Vector2 GetInputDirection()
        {
            if (Input.touchCount == 0) return Vector2.zero;
            
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _start = touch.position;
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    _end = touch.position;
                    _direction = (_end - _start).normalized;
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _direction;
        }
    }
}