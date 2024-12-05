using Assets.Scripts.Objects;

namespace Assets.Scripts.SetGameEnemy
{
    public class SetGameEnemyDeadState : IObjectState
    {
        private SetGameEnemyDead setGameEnemyDead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            setGameEnemyDead = objectStateMachine.GetComponent<SetGameEnemyDead>();
            setGameEnemyDead.enabled = true;
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
