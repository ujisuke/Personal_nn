using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3AttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy3Attack enemy3Attack;
        private EnemyMain enemyMain;
        private Enemy3Animation enemy3Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy3Attack = objectStateMachine.GetComponent<Enemy3Attack>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy3Animation = objectStateMachine.GetComponent<Enemy3Animation>();
            enemy3Attack.enabled = true;
            enemy3Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy3CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy3DeadState());
            else if(!enemy3Attack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy3MoveState());
        }

        public void Exit()
        {
            enemy3Attack.enabled = false;
            enemy3Attack.StopAttack();
            enemy3Animation.StopAttack();
        }
    }
}