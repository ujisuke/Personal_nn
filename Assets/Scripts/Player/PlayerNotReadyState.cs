using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerNotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Player player;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            player = objectStateMachine.GetComponent<Player>();
        }

        public void FixedUpdate()
        {
            if(player.IsReady)
                objectStateMachine.TransitionTo(new PlayerMoveState());
        }

        public void Exit()
        {

        }
    }
}
