using Assets.Scripts.Objects;

namespace Assets.Scripts.ResetDataEnemy
{
    public class ResetDataEnemyDeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            objectStateMachine.GetComponent<ResetDataEnemyDead>().enabled = true;
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
