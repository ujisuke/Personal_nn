using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private PlayerMove playerMove;
        private Player player;
        private PlayerAnimation playerAnimation;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            playerMove = objectStateMachine.GetComponent<PlayerMove>();
            player = objectStateMachine.GetComponent<Player>();
            playerAnimation = objectStateMachine.GetComponent<PlayerAnimation>();
            playerMove.enabled = true;
            playerAnimation.StartWalk();
        }

        public void FixedUpdate()
        {
            playerAnimation.SetLookingDirection(playerMove.GetLookingDirection());
            if(player.IsDead())
                objectStateMachine.TransitionTo(new PlayerDeadState());
            if(playerMove.CanDash())
                objectStateMachine.TransitionTo(new PlayerDashState());
            if(playerMove.CanAttack())
                objectStateMachine.TransitionTo(new PlayerAttackState());
        }

        public void Exit()
        {
            playerMove.enabled = false;
            playerAnimation.StopWalk();
        }
    }
}
