using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy3MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy3Move enemy3Move;
        private Enemy3 enemy3;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy3Move = objectStateMachine.GetComponent<Enemy3Move>();
            enemy3 = objectStateMachine.GetComponent<Enemy3>();
            enemy3Move.enabled = true;
        }

        public void FixedUpdate()
        {
            if(enemy3.IsDead())
                objectStateMachine.TransitionTo(new Enemy3DeadState());
            if(enemy3Move.CanAttack)
                objectStateMachine.TransitionTo(new Enemy3AttackState());
        }

        public void Exit()
        {
            enemy3Move.enabled = false;
        }
    }
}
