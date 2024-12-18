namespace Assets.Scripts.UI
{
    public class PoseMainState : IPoseState
    {
        PoseStateMachine poseStateMachine;
        private PoseMain poseMain;

        public void Enter(PoseStateMachine poseStateMachine)
        {
            this.poseStateMachine = poseStateMachine;
            poseMain = this.poseStateMachine.GetComponent<PoseMain>();
            poseMain.enabled = true;
        }

        public void Update()
        {
            if (poseMain.IsSetVolumeSelected)
                poseStateMachine.TransitionTo(new PoseVolumeSetterState());
            else if (poseMain.IsBackToLobbySelected)
                poseStateMachine.TransitionTo(new PoseBackerToLobbyState());
            else if (poseMain.IsSetSeedSelected)
                poseStateMachine.TransitionTo(new PoseSeedSetterState());
            else if (poseMain.IsExitGameSelected)
                poseStateMachine.TransitionTo(new PoseGameExiterState());
        }

        public void Exit()
        {
            poseMain.enabled = false;
        }
    }
}