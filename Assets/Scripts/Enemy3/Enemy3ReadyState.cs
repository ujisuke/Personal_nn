using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3ReadyState : IObjectState
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
                objectStateMachine.TransitionTo(new Enemy3MoveState());
        }

        public void Exit()
        {

        }
    }
}
