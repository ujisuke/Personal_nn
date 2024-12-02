using Assets.Scripts.Objects;

namespace Assets.Scripts.ExitEnemy
{
    public class ExitEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private ExitEnemyMove exitEnemyMove;
        private ExitEnemyMain exitEnemyMain;
        private ExitEnemyAnimation exitEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            exitEnemyMove = objectStateMachine.GetComponent<ExitEnemyMove>();
            exitEnemyMain = objectStateMachine.GetComponent<ExitEnemyMain>();
            exitEnemyAnimation = objectStateMachine.GetComponent<ExitEnemyAnimation>();
            exitEnemyMove.enabled = true;
            exitEnemyAnimation.StartStand();
        }

        public void FixedUpdate()
        {
            if(exitEnemyMain.IsDead())
                objectStateMachine.TransitionTo(new ExitEnemyDeadState());
        }

        public void Exit()
        {
            exitEnemyMove.enabled = false;
            exitEnemyAnimation.StopStand();
        }
    }
}
