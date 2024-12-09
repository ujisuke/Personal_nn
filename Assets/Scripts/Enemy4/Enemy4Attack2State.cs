using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4Attack2State : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy4Attack2 enemy4Attack2;
        private EnemyMain enemyMain;
        private Enemy4Animation enemy4Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy4Attack2 = objectStateMachine.GetComponent<Enemy4Attack2>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy4Animation = objectStateMachine.GetComponent<Enemy4Animation>();
            enemy4Attack2.enabled = true;
            enemy4Animation.SetLookingDirection(ObjectMove.GetLookingAtPlayerDirection(objectStateMachine.transform.position));
            enemy4Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy4CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy4DeadState());
            else if(!enemy4Attack2.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy4Move1State());
        }

        public void Exit()
        {
            enemy4Attack2.StopAttack();
            enemy4Attack2.enabled = false;
            enemy4Animation.StopAttack();
        }
    }
}
