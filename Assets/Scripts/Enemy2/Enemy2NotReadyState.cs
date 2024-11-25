using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2NotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2Main enemy2Main;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2Main = objectStateMachine.GetComponent<Enemy2Main>();
        }

        public void FixedUpdate()
        {
            if(enemy2Main.IsReady)
                objectStateMachine.TransitionTo(new Enemy2MoveState());
        }

        public void Exit()
        {

        }
    }
}
