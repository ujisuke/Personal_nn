using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy3NotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy3 enemy3;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy3 = objectStateMachine.GetComponent<Enemy3>();
        }

        public void FixedUpdate()
        {
            if(enemy3.IsReady)
                objectStateMachine.TransitionTo(new Enemy3MoveState());
        }

        public void Exit()
        {

        }
    }
}
