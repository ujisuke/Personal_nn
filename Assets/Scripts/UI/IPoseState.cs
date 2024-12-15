namespace Assets.Scripts.UI
{
    public interface IPoseState
    {
        public void Enter(PoseStateMachine poseStateMachine);
        public void Update();
        public void Exit();
    }
}