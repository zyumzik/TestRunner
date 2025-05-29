using Core.FiniteStateMachine;
using DG.Tweening;
using UnityEngine;

namespace PlayerModule.PlayerStates
{
    public class JumpState : State
    {
        private readonly PlayerAnimatorController _animatorController;
        private readonly Collider _jumpCollider;

        private Tween _jumpTween;
        
        public JumpState(PlayerAnimatorController animatorController, Collider jumpCollider)
        {
            _animatorController = animatorController;
            _jumpCollider = jumpCollider;
        }

        public override void Enter()
        {
            _jumpCollider.enabled = true;
            _animatorController.PlayJump();
        }

        public override void Exit()
        {
            _jumpCollider.enabled = false;
        }
    }
}