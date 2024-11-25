using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3NotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy3Main enemy3Move;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy3Move = objectStateMachine.GetComponent<Enemy3Main>();
        }

        public void FixedUpdate()
        {
            if(enemy3Move.IsReady)
                objectStateMachine.TransitionTo(new Enemy3MoveState());
        }

        public void Exit()
        {

        }
    }
}
