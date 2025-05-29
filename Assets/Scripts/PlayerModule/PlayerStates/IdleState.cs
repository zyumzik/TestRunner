using Core.FiniteStateMachine;

namespace PlayerModule.PlayerStates
{
    public class IdleState : State
    {
        private readonly PlayerAnimatorController _animatorController;
        
        public IdleState(PlayerAnimatorController animatorController)
        {
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            _animatorController.PlayIdle();
        }
    }
}