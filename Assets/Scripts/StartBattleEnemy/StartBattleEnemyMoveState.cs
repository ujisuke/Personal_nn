using Assets.Scripts.Objects;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private StartBattleEnemyMove startBattleEnemyMove;
        private StartBattleEnemyMain startBattleEnemyMain;
        private StartBattleEnemyAnimation startBattleEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            startBattleEnemyMove = objectStateMachine.GetComponent<StartBattleEnemyMove>();
            startBattleEnemyMain = objectStateMachine.GetComponent<StartBattleEnemyMain>();
            startBattleEnemyAnimation = objectStateMachine.GetComponent<StartBattleEnemyAnimation>();
            startBattleEnemyMove.enabled = true;
            startBattleEnemyAnimation.StartWalk();
        }

        public void FixedUpdate()
        {
            if(startBattleEnemyMain.IsDead())
                objectStateMachine.TransitionTo(new StartBattleEnemyDeadState());
        }

        public void Exit()
        {
            startBattleEnemyMove.enabled = false;
            startBattleEnemyAnimation.StopWalk();
        }
    }
}
