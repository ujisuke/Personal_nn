using Assets.Scripts.Objects;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyDeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            objectStateMachine.GetComponent<StartBattleEnemyDead>().enabled = true;
            objectStateMachine.GetComponent<StartBattleEnemyAnimation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
