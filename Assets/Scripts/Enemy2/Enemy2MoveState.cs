using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2Move enemy2Move;
        private EnemyMain enemyMain;
        private Enemy2Animation enemy2Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2Move = objectStateMachine.GetComponent<Enemy2Move>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy2Animation = objectStateMachine.GetComponent<Enemy2Animation>();
            enemy2Move.enabled = true;
            enemy2Animation.StartWalk();
        }

        public void FixedUpdate()
        {
            enemy2Animation.SetLookingDirection(enemy2Move.GetLookingDirection());
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy2CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy2DeadState());
            else if(enemy2Move.IsMissingPlayer())
                objectStateMachine.TransitionTo(new Enemy2MissingPlayerState());
            else if(enemy2Move.CanAttack())
                objectStateMachine.TransitionTo(new Enemy2AttackState());
        }

        public void Exit()
        {
            enemy2Move.enabled = false;
            enemy2Animation.StopMove();
        }
    }
}
