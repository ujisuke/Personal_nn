using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy2MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2Move enemy2Move;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2Move = objectStateMachine.GetComponent<Enemy2Move>();
            enemy2Move.enabled = true;
        }

        public void FixedUpdate()
        {
            if(enemy2Move.CanAttack())
                objectStateMachine.TransitionTo(new Enemy2AttackState());
        }

        public void Exit()
        {
            enemy2Move.enabled = false;
        }
    }
}
