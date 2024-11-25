using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2MissingPlayerState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2MissingPlayer enemy2MissingPlayer;
        private Enemy2Main enemy2Main;
        private Enemy2Animation enemy2Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2MissingPlayer = objectStateMachine.GetComponent<Enemy2MissingPlayer>();
            enemy2Main = objectStateMachine.GetComponent<Enemy2Main>();
            enemy2Animation = objectStateMachine.GetComponent<Enemy2Animation>();
            enemy2MissingPlayer.enabled = true;
            enemy2Animation.StartStand();
        }

        public void FixedUpdate()
        {
            if(enemy2Main.IsDead())
                objectStateMachine.TransitionTo(new Enemy2DeadState());
            if(!enemy2MissingPlayer.IsMissingPlayer)
                objectStateMachine.TransitionTo(new Enemy2MoveState());
        }

        public void Exit()
        {
            enemy2MissingPlayer.enabled = false;
            enemy2Animation.StopStand();
        }
    }
}
