using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy3Move enemy3Move;
        private Enemy3Main enemy3Main;
        private Enemy3Animation enemy3Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy3Move = objectStateMachine.GetComponent<Enemy3Move>();
            enemy3Main = objectStateMachine.GetComponent<Enemy3Main>();
            enemy3Animation = objectStateMachine.GetComponent<Enemy3Animation>();
            enemy3Move.enabled = true;
            enemy3Animation.StartStand();
        }

        public void FixedUpdate()
        {
            enemy3Animation.SetLookingDirection(enemy3Move.GetLookingDirection());
            if(enemy3Main.IsDead())
                objectStateMachine.TransitionTo(new Enemy3DeadState());
            if(enemy3Move.CanAttack)
                objectStateMachine.TransitionTo(new Enemy3AttackState());
        }

        public void Exit()
        {
            enemy3Move.enabled = false;
            enemy3Animation.StopStand();
        }
    }
}