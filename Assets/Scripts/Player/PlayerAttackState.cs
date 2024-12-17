using Assets.Scripts.Objects;
using Assets.Scripts.Sounds;

namespace Assets.Scripts.Player
{
    public class PlayerAttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private PlayerMain playerMain;
        private PlayerAttack playerAttack;
        private PlayerAnimation playerAnimation;
        private PlayerAttackEffectAnimation playerAttackEffectAnimation;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            playerMain = objectStateMachine.GetComponent<PlayerMain>();
            playerAttack = objectStateMachine.GetComponent<PlayerAttack>();
            playerAnimation = objectStateMachine.GetComponent<PlayerAnimation>();
            playerAttackEffectAnimation = objectStateMachine.GetComponentInChildren<PlayerAttackEffectAnimation>();
            playerAttack.enabled = true;
            playerAnimation.StartAttack();
            playerAttackEffectAnimation.StartAttack();
            SEPlayer.SingletonInstance.PlayAttack();
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
        }
    }
}
