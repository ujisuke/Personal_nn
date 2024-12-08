using Assets.Scripts.Objects;

namespace Assets.Scripts.UIEnemies
{
    public class UIEnemyReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private EnemyMain enemyMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            ObjectStorage.AddEnemy(objectStateMachine.GetComponent<EnemyMain>());
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsReady)
                objectStateMachine.TransitionTo(new UIEnemyMoveState());
        }

        public void Exit()
        {

        }
    }
}
