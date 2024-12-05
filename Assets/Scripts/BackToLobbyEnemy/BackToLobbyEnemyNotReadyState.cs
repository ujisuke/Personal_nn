using Assets.Scripts.Objects;

namespace Assets.Scripts.BackToLobbyEnemy
{
    public class BackToLobbyEnemyNotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private BackToLobbyEnemyMain backToLobbyEnemyMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            backToLobbyEnemyMain = objectStateMachine.GetComponent<BackToLobbyEnemyMain>();
        }

        public void FixedUpdate()
        {
            if(backToLobbyEnemyMain.IsReady)
                objectStateMachine.TransitionTo(new BackToLobbyEnemyMoveState());
        }

        public void Exit()
        {

        }
    }
}
