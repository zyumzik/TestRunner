using Core.FiniteStateMachine;

namespace PlayerModule.PlayerStates
{
    public class DeathState : State
    {
        private readonly PlayerAnimatorController _animatorController;

        public DeathState(PlayerAnimatorController animatorController)
        {
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            _animatorController.PlayDeath();
        }
    }
}