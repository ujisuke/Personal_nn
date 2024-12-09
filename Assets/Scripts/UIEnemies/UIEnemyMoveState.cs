using Assets.Scripts.Objects;

namespace Assets.Scripts.UIEnemies
{
    public class UIEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private UIEnemyMove uIEnemyMove;
        private EnemyMain enemyMain;
        private UIEnemyAnimation uIEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            uIEnemyMove = objectStateMachine.GetComponent<UIEnemyMove>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            uIEnemyAnimation = objectStateMachine.GetComponent<UIEnemyAnimation>();
            uIEnemyMove.enabled = true;
            uIEnemyAnimation.StartMove();
        }

        public void FixedUpdate()
        {
            uIEnemyAnimation.SetLookingDirection(ObjectMove.GetLookingAtPlayerDirection(objectStateMachine.transform.position));
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new UIEnemyCleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new UIEnemyDeadState());
        }

        public void Exit()
        {
            uIEnemyMove.enabled = false;
            uIEnemyAnimation.StopMove();
        }
    }
}
