using Assets.Scripts.Objects;

namespace Assets.Scripts.ResetEnemy
{
    public class ResetEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private ResetEnemyMove resetEnemyMove;
        private ResetEnemyMain resetEnemyMain;
        private ResetEnemyAnimation resetEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            resetEnemyMove = objectStateMachine.GetComponent<ResetEnemyMove>();
            resetEnemyMain = objectStateMachine.GetComponent<ResetEnemyMain>();
            resetEnemyAnimation = objectStateMachine.GetComponent<ResetEnemyAnimation>();
            resetEnemyMove.enabled = true;
            resetEnemyAnimation.StartStand();
        }

        public void FixedUpdate()
        {
            if(resetEnemyMain.IsDead())
                objectStateMachine.TransitionTo(new ResetEnemyDeadState());
        }

        public void Exit()
        {
            resetEnemyMove.enabled = false;
            resetEnemyAnimation.StopStand();
        }
    }
}
