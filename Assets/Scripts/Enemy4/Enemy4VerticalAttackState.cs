using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4VerticalAttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy4VerticalAttack enemy4VerticalAttack;
        private EnemyMain enemyMain;
        private Enemy4Animation enemy4Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy4VerticalAttack = objectStateMachine.GetComponent<Enemy4VerticalAttack>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy4Animation = objectStateMachine.GetComponent<Enemy4Animation>();
            enemy4VerticalAttack.enabled = true;
            enemy4Animation.SetLookingDirection(ObjectMove.GetLookingAtPlayerDirection(objectStateMachine.transform.position));
            enemy4Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy4CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy4DeadState());
            else if(!enemy4VerticalAttack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy4HorizontalMoveState());
        }

        public void Exit()
        {
            enemy4VerticalAttack.StopAttack();
            enemy4VerticalAttack.enabled = false;
            enemy4Animation.StopAttack();
        }
    }
}
