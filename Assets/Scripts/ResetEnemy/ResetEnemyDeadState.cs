using Assets.Scripts.Objects;

namespace Assets.Scripts.ResetEnemy
{
    public class ResetEnemyDeadState : IObjectState
    {
        private ResetEnemyDead resetEnemyDead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            resetEnemyDead = objectStateMachine.GetComponent<ResetEnemyDead>();
            resetEnemyDead.enabled = true;
            objectStateMachine.GetComponent<ResetEnemyAnimation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
