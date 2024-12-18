using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PoseSeedSetterState : IPoseState
    {
        private PoseStateMachine poseStateMachine;
        private PoseSeedSetter poseSeedSetter;

        public void Enter(PoseStateMachine poseStateMachine)
        {
            this.poseStateMachine = poseStateMachine;
            poseSeedSetter = this.poseStateMachine.GetComponent<PoseSeedSetter>();
            poseSeedSetter.enabled = true;
        }

        public void Update()
        {
            if (poseSeedSetter.IsReturnSelected || Input.GetKey(KeyCode.Escape))
                poseStateMachine.TransitionTo(new PoseMainState());
        }

        public void Exit()
        {
            poseSeedSetter.enabled = false;
        }
    }
}