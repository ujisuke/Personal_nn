using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1NotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy1 enemy1;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy1 = objectStateMachine.GetComponent<Enemy1>();
        }

        public void FixedUpdate()
        {
            if(enemy1.IsReady)
                objectStateMachine.TransitionTo(new Enemy1MoveState());
        }

        public void Exit()
        {

        }
    }
}
