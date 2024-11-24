using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy1Move enemy1Move;
        private Enemy1 enemy1;
        private Enemy1Animation enemy1Animation;
        private ObjectMove objectMove;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy1Move = objectStateMachine.GetComponent<Enemy1Move>();
            enemy1 = objectStateMachine.GetComponent<Enemy1>();
            enemy1Animation = objectStateMachine.GetComponent<Enemy1Animation>();
            objectMove = objectStateMachine.GetComponent<ObjectMove>();
            enemy1Move.enabled = true;
            enemy1Animation.StartWalk();
        }

        public void FixedUpdate()
        {
            enemy1Animation.SetHeadingDirection(objectMove.GetHeadingDirection());
            if(enemy1.IsDead())
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
