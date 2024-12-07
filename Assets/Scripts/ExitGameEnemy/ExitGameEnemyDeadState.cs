using Assets.Scripts.Objects;

namespace Assets.Scripts.ExitGameEnemy
{
    public class ExitGameEnemyDeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            objectStateMachine.GetComponent<ExitGameEnemyDead>().enabled = true;
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
