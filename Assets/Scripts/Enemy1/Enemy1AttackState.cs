using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1AttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy1Attack enemy1Attack;
        private EnemyMain enemyMain;
        private Enemy1Animation enemy1Animation;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy1Attack = objectStateMachine.GetComponent<Enemy1Attack>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy1Animation = objectStateMachine.GetComponent<Enemy1Animation>();
            enemy1Attack.enabled = true;
            enemy1Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy1CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy1DeadState());      
            else if(!enemy1Attack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy1MoveState());
        }

        public void Exit()
        {
            enemy1Attack.enabled = false;
            enemy1Animation.StopAttack();
        }
    }
}
