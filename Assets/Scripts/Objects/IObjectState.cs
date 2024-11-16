namespace Assets.Scripts.Objects
{
    public interface IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine);
        public void FixedUpdate();
        public void Exit();
    }
}
