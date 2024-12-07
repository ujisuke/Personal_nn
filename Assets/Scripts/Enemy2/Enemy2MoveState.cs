using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2Move enemy2Move;
        private Enemy2Main enemy2Main;
        private Enemy2Animation enemy2Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2Move = objectStateMachine.GetComponent<Enemy2Move>();
            enemy2Main = objectStateMachine.GetComponent<Enemy2Main>();
            enemy2Animation = objectStateMachine.GetComponent<Enemy2Animation>();
            enemy2Move.enabled = true;
            enemy2Animation.StartWalk();
        }

        public void FixedUpdate()
        {
            enemy2Animation.SetLookingDirection(enemy2Move.GetLookingDirection());
            if(enemy2Main.IsDead())
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
