namespace Assets.Scripts.UI
{
    public class PoseGameExiterState : IPoseState
    {
        private PoseStateMachine poseStateMachine;
        private PoseGameExiter poseGameExiter;

        public void Enter(PoseStateMachine poseStateMachine)
        {
            this.poseStateMachine = poseStateMachine;
            poseGameExiter = this.poseStateMachine.GetComponent<PoseGameExiter>();
            poseGameExiter.enabled = true;
        }

        public void Update()
        {
            poseStateMachine.TransitionTo(new PoseMainState());
        }

        public void Exit()
        {
            poseGameExiter.enabled = false;
            CanvasStorage.HidePoseCanvas();
        }
    }
}