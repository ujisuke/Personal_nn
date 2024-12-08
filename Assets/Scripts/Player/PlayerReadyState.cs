using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private PlayerMain playerMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            ObjectStorage.AddPlayer(objectStateMachine.GetComponent<PlayerMain>());
            playerMain = objectStateMachine.GetComponent<PlayerMain>();
        }

        public void FixedUpdate()
        {
            if(playerMain.IsReady)
                objectStateMachine.TransitionTo(new PlayerMoveState());
        }

        public void Exit()
        {

        }
    }
}
