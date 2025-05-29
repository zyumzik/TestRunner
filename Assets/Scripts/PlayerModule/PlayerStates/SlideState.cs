using Core.FiniteStateMachine;
using UnityEngine;

namespace PlayerModule.PlayerStates
{
    public class SlideState : State
    {
        private readonly PlayerAnimatorController _animatorController;
        private readonly Collider _slideCollider;

        public SlideState(PlayerAnimatorController animatorController, Collider slideCollider)
        {
            _animatorController = animatorController;
            _slideCollider = slideCollider;
        }

        public override void Enter()
        {
            _slideCollider.enabled = true;
            _animatorController.PlaySlide();
        }

        public override void Exit()
        {
            _slideCollider.enabled = false;
        }
    }
}