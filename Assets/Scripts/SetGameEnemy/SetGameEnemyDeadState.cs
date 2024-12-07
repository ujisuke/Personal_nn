using Assets.Scripts.Objects;

namespace Assets.Scripts.SetGameEnemy
{
    public class SetGameEnemyDeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            objectStateMachine.GetComponent<SetGameEnemyDead>().enabled = true;
            objectStateMachine.GetComponent<SetGameEnemyAnimation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
