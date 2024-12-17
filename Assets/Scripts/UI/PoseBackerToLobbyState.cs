namespace Assets.Scripts.UI
{
    public class PoseBackerToLobbyState : IPoseState
    {
        private PoseStateMachine poseStateMachine;
        private PoseBackerToLobby poseBackerToLobby;

        public void Enter(PoseStateMachine poseStateMachine)
        {
            this.poseStateMachine = poseStateMachine;
            poseBackerToLobby = this.poseStateMachine.GetComponent<PoseBackerToLobby>();
            poseBackerToLobby.enabled = true;
        }

        public void Update()
        {
            poseStateMachine.TransitionTo(new PoseMainState());
        }

        public void Exit()
        {
            poseBackerToLobby.enabled = false;
            CanvasStorage.SingletonInstance.HidePoseCanvas();
        }
    }
}
