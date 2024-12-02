using Assets.Scripts.Objects;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyDeadState : IObjectState
    {
        private StartBattleEnemyDead startBattleEnemyDead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            startBattleEnemyDead = objectStateMachine.GetComponent<StartBattleEnemyDead>();
            startBattleEnemyDead.enabled = true;
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
