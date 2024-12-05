using Assets.Scripts.Objects;

namespace Assets.Scripts.ExitGameEnemy
{
    public class ExitEnemyNotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private ExitGameEnemyMain exitGameEnemyMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            exitGameEnemyMain = objectStateMachine.GetComponent<ExitGameEnemyMain>();
        }

        public void FixedUpdate()
        {
            if(exitGameEnemyMain.IsReady)
                objectStateMachine.TransitionTo(new ExitGameEnemyMoveState());
        }

        public void Exit()
        {

        }
    }
}
