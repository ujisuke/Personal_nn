using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4Move1State : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy4Move enemy4Move;
        private EnemyMain enemyMain;
        private Enemy4Animation enemy4Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy4Move = objectStateMachine.GetComponent<Enemy4Move>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy4Animation = objectStateMachine.GetComponent<Enemy4Animation>();
            enemy4Move.enabled = true;
            enemy4Animation.StartWalk();
        }

        public void FixedUpdate()
        {
            enemy4Animation.SetLookingDirection(ObjectMove.GetLookingAtPlayerDirection(objectStateMachine.transform.position));
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy4CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy4DeadState());
            else if(enemy4Move.CanAttack)
                objectStateMachine.TransitionTo(new Enemy4Attack1State());
        }

        public void Exit()
        {
            enemy4Move.enabled = false;
            enemy4Animation.StopMove();
        }
    }
}
