using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2MissingPlayerState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2MissingPlayer enemy2MissingPlayer;
        private EnemyMain enemyMain;
        private Enemy2Animation enemy2Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2MissingPlayer = objectStateMachine.GetComponent<Enemy2MissingPlayer>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy2Animation = objectStateMachine.GetComponent<Enemy2Animation>();
            enemy2MissingPlayer.enabled = true;
            enemy2Animation.StartStand();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy2CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy2DeadState());
            else if(!enemy2MissingPlayer.IsMissingPlayer)
                objectStateMachine.TransitionTo(new Enemy2MoveState());
        }

        public void Exit()
        {
            enemy2MissingPlayer.StopMissingPlayer();
            enemy2MissingPlayer.enabled = false;
            enemy2Animation.StopMissingPlayer();
        }
    }
}
