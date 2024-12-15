using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PoseVolumeSetterState : IPoseState
    {
        private PoseStateMachine poseStateMachine;
        private PoseVolumeSetter poseVolumeSetter;

        public void Enter(PoseStateMachine poseStateMachine)
        {
            this.poseStateMachine = poseStateMachine;
            poseVolumeSetter = this.poseStateMachine.GetComponent<PoseVolumeSetter>();
            poseVolumeSetter.enabled = true;
        }

        public void Update()
        {
            if (poseVolumeSetter.IsReturnSelected || Input.GetKey(KeyCode.Escape))
                poseStateMachine.TransitionTo(new PoseMainState());
        }

        public void Exit()
        {
            poseVolumeSetter.enabled = false;
        }
    }
}