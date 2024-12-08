using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerAttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private PlayerMain playerMain;
        private PlayerAttack playerAttack;
        private PlayerAnimation playerAnimation;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            playerMain = objectStateMachine.GetComponent<PlayerMain>();
            playerAttack = objectStateMachine.GetComponent<PlayerAttack>();
            playerAnimation = objectStateMachine.GetComponent<PlayerAnimation>();
            playerAttack.enabled = true;
            playerAnimation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(playerMain.IsCleaned)
                objectStateMachine.TransitionTo(new PlayerCleanedState());
            else if(playerMain.IsDead())
                objectStateMachine.TransitionTo(new PlayerDeadState());
            else if(!playerAttack.IsAttacking)
                objectStateMachine.TransitionTo(new PlayerMoveState());
        }

        public void Exit()
        {
            playerAttack.enabled = false;
            playerAnimation.StopAttack();
        }
    }
}
