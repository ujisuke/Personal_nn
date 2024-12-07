using Assets.Scripts.Objects;

namespace Assets.Scripts.ResetDataEnemy
{
    public class ResetDataEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private ResetDataEnemyMove resetDataEnemyMove;
        private ResetDataEnemyMain resetDataEnemyMain;
        private ResetDataEnemyAnimation resetDataEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            resetDataEnemyMove = objectStateMachine.GetComponent<ResetDataEnemyMove>();
            resetDataEnemyMain = objectStateMachine.GetComponent<ResetDataEnemyMain>();
            resetDataEnemyAnimation = objectStateMachine.GetComponent<ResetDataEnemyAnimation>();
            resetDataEnemyMove.enabled = true;
            resetDataEnemyAnimation.StartMove();
        }

        public void FixedUpdate()
        {
            if(resetDataEnemyMain.IsDead())
                objectStateMachine.TransitionTo(new ResetDataEnemyDeadState());
        }

        public void Exit()
        {
            resetDataEnemyMove.enabled = false;
            resetDataEnemyAnimation.StopMove();
        }
    }
}
