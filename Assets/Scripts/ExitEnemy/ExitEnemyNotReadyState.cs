using Assets.Scripts.Objects;

namespace Assets.Scripts.ExitEnemy
{
    public class ExitEnemyNotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private ExitEnemyMain exitEnemyMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            exitEnemyMain = objectStateMachine.GetComponent<ExitEnemyMain>();
        }

        public void FixedUpdate()
        {
            if(exitEnemyMain.IsReady)
                objectStateMachine.TransitionTo(new ExitEnemyMoveState());
        }

        public void Exit()
        {

        }
    }
}
