using Assets.Scripts.Objects;

namespace Assets.Scripts.SettingEnemy
{
    public class SettingEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private SettingEnemyMove settingEnemyMove;
        private SettingEnemyMain settingEnemyMain;
        private SettingEnemyAnimation settingEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            settingEnemyMove = objectStateMachine.GetComponent<SettingEnemyMove>();
            settingEnemyMain = objectStateMachine.GetComponent<SettingEnemyMain>();
            settingEnemyAnimation = objectStateMachine.GetComponent<SettingEnemyAnimation>();
            settingEnemyMove.enabled = true;
            settingEnemyAnimation.StartWalk();
        }

        public void FixedUpdate()
        {
            if(settingEnemyMain.IsDead())
                objectStateMachine.TransitionTo(new SettingEnemyDeadState());
        }

        public void Exit()
        {
            settingEnemyMove.enabled = false;
            settingEnemyAnimation.StopWalk();
        }
    }
}
