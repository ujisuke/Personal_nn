using Assets.Scripts.Objects;

namespace Assets.Scripts.UIEnemies
{
    public class UIEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private UIEnemyMove uiEnemyMove;
        private EnemyMain enemyMain;
        private UIEnemyAnimation uiEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            uiEnemyMove = objectStateMachine.GetComponent<UIEnemyMove>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            uiEnemyAnimation = objectStateMachine.GetComponent<UIEnemyAnimation>();
            uiEnemyMove.enabled = true;
            uiEnemyAnimation.StartMove();
        }

        public void FixedUpdate()
        {
            uiEnemyAnimation.SetLookingDirection(ObjectMove.GetLookingAtPlayerDirection(objectStateMachine.transform.position));
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new UIEnemyCleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new UIEnemyDeadState());
        }

        public void Exit()
        {
            uiEnemyMove.enabled = false;
            uiEnemyAnimation.StopMove();
        }
    }
}
