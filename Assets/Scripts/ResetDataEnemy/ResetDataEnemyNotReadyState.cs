using Assets.Scripts.Objects;

namespace Assets.Scripts.ResetDataEnemy
{
    public class ResetDataEnemyNotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private ResetDataEnemyMain resetDataEnemyMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            resetDataEnemyMain = objectStateMachine.GetComponent<ResetDataEnemyMain>();
        }

        public void FixedUpdate()
        {
            if(resetDataEnemyMain.IsReady)
                objectStateMachine.TransitionTo(new ResetDataEnemyMoveState());
        }

        public void Exit()
        {

        }
    }
}
