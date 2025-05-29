using UnityEngine;

namespace PlayerModule.PlayerInputModule
{
    public class EditorInputStrategy : IPlayerInputStrategy
    {
        public Vector2 GetInputDirection()
        {
            float horizontal = 0;
            float vertical = 0;
            
            if (Input.GetKeyDown(KeyCode.A)) horizontal -= 1;
            if (Input.GetKeyDown(KeyCode.D)) horizontal += 1;
            if (Input.GetKeyDown(KeyCode.W)) vertical += 1;
            if (Input.GetKeyDown(KeyCode.S)) vertical -= 1;
            
            return new Vector2(horizontal, vertical).normalized;
        }
    }
}