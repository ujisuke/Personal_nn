using Assets.Scripts.Objects;

namespace Assets.Scripts.ExitGameEnemy
{
    public class ExitGameEnemyDeadState : IObjectState
    {
        private ExitGameEnemyDead exitGameEnemyDead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            exitGameEnemyDead = objectStateMachine.GetComponent<ExitGameEnemyDead>();
            exitGameEnemyDead.enabled = true;
            objectStateMachine.GetComponent<ExitGameEnemyAnimation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
