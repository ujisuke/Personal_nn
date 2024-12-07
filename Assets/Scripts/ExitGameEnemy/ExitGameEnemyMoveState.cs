using Assets.Scripts.Objects;

namespace Assets.Scripts.ExitGameEnemy
{
    public class ExitGameEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private ExitGameEnemyMove exitGameEnemyMove;
        private ExitGameEnemyMain exitGameEnemyMain;
        private ExitGameEnemyAnimation exitGameEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            exitGameEnemyMove = objectStateMachine.GetComponent<ExitGameEnemyMove>();
            exitGameEnemyMain = objectStateMachine.GetComponent<ExitGameEnemyMain>();
            exitGameEnemyAnimation = objectStateMachine.GetComponent<ExitGameEnemyAnimation>();
            exitGameEnemyMove.enabled = true;
            exitGameEnemyAnimation.StartMove();
        }

        public void FixedUpdate()
        {
            if(exitGameEnemyMain.IsDead())
                objectStateMachine.TransitionTo(new ExitGameEnemyDeadState());
        }

        public void Exit()
        {
            exitGameEnemyMove.enabled = false;
            exitGameEnemyAnimation.StopMove();
        }
    }
}
