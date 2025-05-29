namespace Core.FiniteStateMachine
{
    public interface IState
    {
        void Enter();
        void Update();
        void FixedUpdate();
        void LateUpdate();
        void Exit();
    }
}
