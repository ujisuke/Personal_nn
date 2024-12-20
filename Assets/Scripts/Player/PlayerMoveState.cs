using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerMoveState : IObjectState
    {
        private PlayerMain playerMain;
        private ObjectStateMachine objectStateMachine;
        private PlayerMove playerMove;
        private PlayerAnimation playerAnimation;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            playerMain = objectStateMachine.GetComponent<PlayerMain>();
            playerMove = objectStateMachine.GetComponent<PlayerMove>();
            playerAnimation = objectStateMachine.GetComponent<PlayerAnimation>();
            playerMove.enabled = true;
            playerAnimation.StartMove();
        }

        public void FixedUpdate()
        {
            playerAnimation.SetLookingDirection(playerMove.GetLookingDirection());
            if(playerMain.IsCleaned)
                objectStateMachine.TransitionTo(new PlayerCleanedState());
            else if(playerMain.IsDead())
                objectStateMachine.TransitionTo(new PlayerDeadState());
            else if(playerMove.CanAttack())
                objectStateMachine.TransitionTo(new PlayerAttackState());
        }

        public void Exit()
        {
            playerMove.enabled = false;
            playerAnimation.StopMove();
        }
    }
}
