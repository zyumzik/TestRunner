using Core.FiniteStateMachine;
using UnityEngine;

namespace PlayerModule.PlayerStates
{
    public class RunningState : State
    {
        private readonly PlayerAnimatorController _animatorController;
        private readonly Collider _runningCollider;
        
        public RunningState(PlayerAnimatorController animatorController, Collider runningCollider)
        {
            _animatorController = animatorController;
            _runningCollider = runningCollider;
        }
        
        public override void Enter()
        {
            _runningCollider.enabled = true;
            _animatorController.PlayRun();
        }

        public override void Exit()
        {
            _runningCollider.enabled = false;
        }
    }
}