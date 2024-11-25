using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy2MissingPlayerState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2MissingPlayer enemy2MissingPlayer;
        private Enemy2 enemy2;
        private Enemy2Animation enemy2Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2MissingPlayer = objectStateMachine.GetComponent<Enemy2MissingPlayer>();
            enemy2 = objectStateMachine.GetComponent<Enemy2>();
            enemy2Animation = objectStateMachine.GetComponent<Enemy2Animation>();
            enemy2MissingPlayer.enabled = true;
            enemy2Animation.StartStand();
        }

        public void FixedUpdate()
        {
            if(enemy2.IsDead())
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
