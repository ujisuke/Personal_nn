using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerAttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private PlayerAttack playerAttack;
        private PlayerAnimation playerAnimation;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            playerAttack = objectStateMachine.GetComponent<PlayerAttack>();
            playerAnimation = objectStateMachine.GetComponent<PlayerAnimation>();
            playerAttack.enabled = true;
            playerAnimation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(PlayerMain.IsDead())
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
