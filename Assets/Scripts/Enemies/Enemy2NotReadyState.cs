using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy2NotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2 enemy2;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2 = objectStateMachine.GetComponent<Enemy2>();
        }

        public void FixedUpdate()
        {
            if(enemy2.IsReady)
                objectStateMachine.TransitionTo(new Enemy2MoveState());
        }

        public void Exit()
        {

        }
    }
}
