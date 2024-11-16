using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy3MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy3Move enemy3Move;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy3Move = objectStateMachine.GetComponent<Enemy3Move>();
            enemy3Move.enabled = true;
        }

        public void FixedUpdate()
        {
            if(enemy3Move.CanAttack)
                objectStateMachine.TransitionTo(new Enemy3AttackState());
        }

        public void Exit()
        {
            enemy3Move.enabled = false;
        }
    }
}
