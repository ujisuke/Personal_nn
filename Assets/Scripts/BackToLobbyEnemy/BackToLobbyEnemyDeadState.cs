using Assets.Scripts.Objects;

namespace Assets.Scripts.BackToLobbyEnemy
{
    public class BackToLobbyEnemyDeadState : IObjectState
    {
        private BackToLobbyEnemyDead backToLobbyEnemyDead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            backToLobbyEnemyDead = objectStateMachine.GetComponent<BackToLobbyEnemyDead>();
            backToLobbyEnemyDead.enabled = true;
            objectStateMachine.GetComponent<BackToLobbyEnemyAnimation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
