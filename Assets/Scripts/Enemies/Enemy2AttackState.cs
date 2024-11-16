using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy2AttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2Attack enemy2Attack;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2Attack = objectStateMachine.GetComponent<Enemy2Attack>();
            enemy2Attack.enabled = true;
        }

        public void FixedUpdate()
        {
            if(!enemy2Attack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy2MoveState());
        }

        public void Exit()
        {
            enemy2Attack.enabled = false;
        }
    }
}
