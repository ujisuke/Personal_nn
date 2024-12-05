using Assets.Scripts.Objects;

namespace Assets.Scripts.ResetEnemy
{
    public class ResetEnemyNotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private ResetEnemyMain resetEnemyMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            resetEnemyMain = objectStateMachine.GetComponent<ResetEnemyMain>();
        }

        public void FixedUpdate()
        {
            if(resetEnemyMain.IsReady)
                objectStateMachine.TransitionTo(new ResetEnemyMoveState());
        }

        public void Exit()
        {

        }
    }
}
