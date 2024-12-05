using Assets.Scripts.Objects;

namespace Assets.Scripts.SettingEnemy
{
    public class SettingEnemyDeadState : IObjectState
    {
        private SettingEnemyDead settingEnemyDead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            settingEnemyDead = objectStateMachine.GetComponent<SettingEnemyDead>();
            settingEnemyDead.enabled = true;
            objectStateMachine.GetComponent<SettingEnemyAnimation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
