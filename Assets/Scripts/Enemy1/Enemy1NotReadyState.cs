using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1NotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy1Main enemy1Main;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy1Main = objectStateMachine.GetComponent<Enemy1Main>();
        }

        public void FixedUpdate()
        {
            if(enemy1Main.IsReady)
                objectStateMachine.TransitionTo(new Enemy1MoveState());
        }

        public void Exit()
        {

        }
    }
}
