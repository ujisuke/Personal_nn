using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1ReadyState : IObjectState
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
                objectStateMachine.TransitionTo(new Enemy1MoveState());
        }

        public void Exit()
        {

        }
    }
}
