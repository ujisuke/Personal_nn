using Assets.Scripts.Objects;
using UnityEngine;

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
            {
                if(Random.Range(0, 2) == 0)
                    objectStateMachine.TransitionTo(new Enemy4HorizontalMoveState());
                else
                    objectStateMachine.TransitionTo(new Enemy4VerticalMoveState());
            }
        }

        public void Exit()
        {

        }
    }
}
