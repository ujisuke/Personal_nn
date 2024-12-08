using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2AttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2Attack enemy2Attack;
        private EnemyMain enemyMain;
        private Enemy2Animation enemy2Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2Attack = objectStateMachine.GetComponent<Enemy2Attack>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy2Animation = objectStateMachine.GetComponent<Enemy2Animation>();
            enemy2Attack.enabled = true;
            enemy2Animation.SetLookingDirection(enemy2Attack.GetLookingDirection());
            enemy2Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy2CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy2DeadState());
            else if(!enemy2Attack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy2MoveState());
        }

        public void Exit()
        {
            enemy2Attack.StopAttack();
            enemy2Attack.enabled = false;
            enemy2Animation.StopAttack();
        }
    }
}
