using Assets.Scripts.Objects;

namespace Assets.Scripts.SettingEnemy
{
    public class SettingEnemyNotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private SettingEnemyMain settingEnemyMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            settingEnemyMain = objectStateMachine.GetComponent<SettingEnemyMain>();
        }

        public void FixedUpdate()
        {
            if(settingEnemyMain.IsReady)
                objectStateMachine.TransitionTo(new SettingEnemyMoveState());
        }

        public void Exit()
        {

        }
    }
}
