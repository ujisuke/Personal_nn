using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy1Move enemy1Move;
        private Enemy1Main enemy1Main;
        private Enemy1Animation enemy1Animation;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy1Move = objectStateMachine.GetComponent<Enemy1Move>();
            enemy1Main = objectStateMachine.GetComponent<Enemy1Main>();
            enemy1Animation = objectStateMachine.GetComponent<Enemy1Animation>();
            enemy1Move.enabled = true;
            enemy1Animation.StartWalk();
        }

        public void FixedUpdate()
        {
            enemy1Animation.SetLookingDirection(enemy1Move.GetLookingDirection());
            if(enemy1Main.IsDead())
                objectStateMachine.TransitionTo(new Enemy1DeadState());
            if(enemy1Move.CanAttack())
                objectStateMachine.TransitionTo(new Enemy1AttackState());
        }

        public void Exit()
        {
            enemy1Move.enabled = false;
            enemy1Animation.StopWalk();
        }
    }
}
