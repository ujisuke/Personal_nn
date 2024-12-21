using Assets.Scripts.Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4ReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private EnemyMain enemyMain;
        private bool isWaiting = false;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            ObjectStorage.AddEnemy(objectStateMachine.GetComponent<EnemyMain>());
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
        }

        public async void FixedUpdate()
        {
            if(enemyMain.IsReady && !isWaiting)
            {
                isWaiting = true;
                await UniTask.Delay(System.TimeSpan.FromSeconds(Random.Range(0.1f, 0.6f)));
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
