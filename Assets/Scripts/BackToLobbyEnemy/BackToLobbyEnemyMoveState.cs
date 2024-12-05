using Assets.Scripts.Objects;

namespace Assets.Scripts.BackToLobbyEnemy
{
    public class BackToLobbyEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private BackToLobbyEnemyMove backToLobbyEnemyMove;
        private BackToLobbyEnemyMain backToLobbyEnemyMain;
        private BackToLobbyEnemyAnimation backToLobbyEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            backToLobbyEnemyMove = objectStateMachine.GetComponent<BackToLobbyEnemyMove>();
            backToLobbyEnemyMain = objectStateMachine.GetComponent<BackToLobbyEnemyMain>();
            backToLobbyEnemyAnimation = objectStateMachine.GetComponent<BackToLobbyEnemyAnimation>();
            backToLobbyEnemyMove.enabled = true;
            backToLobbyEnemyAnimation.StartStand();
        }

        public void FixedUpdate()
        {
            if(backToLobbyEnemyMain.IsDead())
                objectStateMachine.TransitionTo(new BackToLobbyEnemyDeadState());
        }

        public void Exit()
        {
            backToLobbyEnemyMove.enabled = false;
            backToLobbyEnemyAnimation.StopStand();
        }
    }
}
