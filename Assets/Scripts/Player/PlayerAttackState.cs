using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerAttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private PlayerAttack playerAttack;
        private PlayerMain playerMain;
        private PlayerAnimation playerAnimation;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            playerAttack = objectStateMachine.GetComponent<PlayerAttack>();
            playerMain = objectStateMachine.GetComponent<PlayerMain>();
            playerAnimation = objectStateMachine.GetComponent<PlayerAnimation>();
            playerAttack.enabled = true;
            playerAnimation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(playerMain.IsDead())
                objectStateMachine.TransitionTo(new PlayerDeadState());
            if(!playerAttack.IsAttacking)
                objectStateMachine.TransitionTo(new PlayerMoveState());
        }

        public void Exit()
        {
            playerAttack.enabled = false;
            playerAnimation.StopAttack();
        }
    }
}
