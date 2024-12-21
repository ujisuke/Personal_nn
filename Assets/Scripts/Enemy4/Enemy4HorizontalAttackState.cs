using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4HorizontalAttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy4HorizontalAttack enemy4HorizontalAttack;
        private EnemyMain enemyMain;
        private Enemy4Animation enemy4Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy4HorizontalAttack = objectStateMachine.GetComponent<Enemy4HorizontalAttack>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy4Animation = objectStateMachine.GetComponent<Enemy4Animation>();
            enemy4HorizontalAttack.enabled = true;
            enemy4Animation.SetLookingDirection(ObjectMove.GetLookingAtPlayerDirection(objectStateMachine.transform.position));
            enemy4Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy4CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy4DeadState());
            else if(!enemy4HorizontalAttack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy4VerticalMoveState());
        }

        public void Exit()
        {
            enemy4HorizontalAttack.StopAttack();
            enemy4HorizontalAttack.enabled = false;
            enemy4Animation.StopAttack();
        }
    }
}
