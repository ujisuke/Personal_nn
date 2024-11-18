using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy2MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2Move enemy2Move;
        private Enemy2 enemy2;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2Move = objectStateMachine.GetComponent<Enemy2Move>();
            enemy2 = objectStateMachine.GetComponent<Enemy2>();
            enemy2Move.enabled = true;
        }

        public void FixedUpdate()
        {
            if(enemy2.IsDead())
                objectStateMachine.TransitionTo(new Enemy2DeadState());
            if(enemy2Move.CanAttack())
                objectStateMachine.TransitionTo(new Enemy2AttackState());
        }

        public void Exit()
        {
            enemy2Move.enabled = false;
        }
    }
}
