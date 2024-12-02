using Assets.Scripts.Objects;

namespace Assets.Scripts.ExitEnemy
{
    public class ExitEnemyDeadState : IObjectState
    {
        private ExitEnemyDead exitEnemyDead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            exitEnemyDead = objectStateMachine.GetComponent<ExitEnemyDead>();
            exitEnemyDead.enabled = true;
            objectStateMachine.GetComponent<ExitEnemyAnimation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
