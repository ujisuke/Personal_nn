using Assets.Scripts.Objects;

namespace Assets.Scripts.ResetDataEnemy
{
    public class ResetDataEnemyDeadState : IObjectState
    {
        private ResetDataEnemyDead resetDataEnemyDead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            resetDataEnemyDead = objectStateMachine.GetComponent<ResetDataEnemyDead>();
            resetDataEnemyDead.enabled = true;
            objectStateMachine.GetComponent<ResetDataEnemyAnimation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
