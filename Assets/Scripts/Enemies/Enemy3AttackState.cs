using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy3AttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy3Attack enemy3Attack;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy3Attack = objectStateMachine.GetComponent<Enemy3Attack>();
            enemy3Attack.enabled = true;
        }

        public void FixedUpdate()
        {
            if(!enemy3Attack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy3MoveState());
        }

        public void Exit()
        {
            enemy3Attack.enabled = false;
        }
    }
}