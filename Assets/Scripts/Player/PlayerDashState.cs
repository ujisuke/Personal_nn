using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerDashState : IObjectState
    {
        PlayerMain playerMain;
        private ObjectStateMachine objectStateMachine;
        private PlayerDash playerDash;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            playerMain = objectStateMachine.GetComponent<PlayerMain>();
            playerDash = objectStateMachine.GetComponent<PlayerDash>();
            playerDash.enabled = true;
        }

        public void FixedUpdate()
        {
            if(playerMain.IsCleaned)
                objectStateMachine.TransitionTo(new PlayerCleanedState());
            else if(playerMain.IsDead())
                objectStateMachine.TransitionTo(new PlayerDeadState());
            else if(!playerDash.IsDashing)
                objectStateMachine.TransitionTo(new PlayerMoveState());
        }

        public void Exit()
        {
            playerDash.enabled = false;
        }
    }
}
