using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4ReadyState : IObjectState
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
                objectStateMachine.TransitionTo(new Enemy4MoveState());
        }

        public void Exit()
        {

        }
    }
}
