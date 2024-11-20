using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerDashState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private PlayerDash playerDash;
        private Player player;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            playerDash = objectStateMachine.GetComponent<PlayerDash>();
            player = objectStateMachine.GetComponent<Player>();
            playerDash.enabled = true;
        }

        public void FixedUpdate()
        {
            if(player.IsDead())
                objectStateMachine.TransitionTo(new PlayerDeadState());
            if(!playerDash.IsDashing)
                objectStateMachine.TransitionTo(new PlayerMoveState());
        }

        public void Exit()
        {
            playerDash.enabled = false;
        }
    }
}
