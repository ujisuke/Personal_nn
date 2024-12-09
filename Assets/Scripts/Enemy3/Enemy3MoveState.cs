using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy3Move enemy3Move;
        private EnemyMain enemyMain;
        private Enemy3Animation enemy3Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy3Move = objectStateMachine.GetComponent<Enemy3Move>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy3Animation = objectStateMachine.GetComponent<Enemy3Animation>();
            enemy3Move.enabled = true;
            enemy3Animation.StartMove();
        }

        public void FixedUpdate()
        {
            enemy3Animation.SetLookingDirection(ObjectMove.GetLookingAtPlayerDirection(objectStateMachine.transform.position));
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy3CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy3DeadState());
            else if(enemy3Move.CanAttack)
                objectStateMachine.TransitionTo(new Enemy3AttackState());
        }

        public void Exit()
        {
            enemy3Move.StopMove();
            enemy3Move.enabled = false;
            enemy3Animation.StopMove();
        }
    }
}
