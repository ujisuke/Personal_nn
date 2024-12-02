using Assets.Scripts.Objects;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyNotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private StartBattleEnemyMain startBattleEnemyMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            startBattleEnemyMain = objectStateMachine.GetComponent<StartBattleEnemyMain>();
        }

        public void FixedUpdate()
        {
            if(startBattleEnemyMain.IsReady)
                objectStateMachine.TransitionTo(new StartBattleEnemyMoveState());
        }

        public void Exit()
        {

        }
    }
}
