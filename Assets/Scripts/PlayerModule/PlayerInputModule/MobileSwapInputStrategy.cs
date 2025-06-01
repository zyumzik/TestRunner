using System;
using UnityEngine;

namespace PlayerModule.PlayerInputModule
{
    public class MobileSwapInputStrategy : IPlayerInputStrategy
    {
        private Vector2 _start;
        private Vector2 _direction;
        private bool _swipeDetected;

        public Vector2 GetInputDirection()
        {
            if (Input.touchCount == 0)
            {
                if (_swipeDetected)
                {
                    _swipeDetected = false;
                    return _direction;
                }

                return Vector2.zero;
            }

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _start = touch.position;
                    break;

                case TouchPhase.Ended:
                    var end = touch.position;
                    var delta = end - _start;

                    if (delta.magnitude < 50f) return Vector2.zero;

                    _direction = GetSwipeDirection(delta);
                    _swipeDetected = true;
                    break;
            }

            return Vector2.zero;
        }

        private Vector2 GetSwipeDirection(Vector2 delta)
        {
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                return delta.x > 0 ? Vector2.right : Vector2.left;
            else
                return delta.y > 0 ? Vector2.up : Vector2.down;
        }
    }

}