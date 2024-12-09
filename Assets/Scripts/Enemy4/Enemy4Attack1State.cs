using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4Attack1State : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy4Attack1 enemy4Attack1;
        private EnemyMain enemyMain;
        private Enemy4Animation enemy4Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy4Attack1 = objectStateMachine.GetComponent<Enemy4Attack1>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy4Animation = objectStateMachine.GetComponent<Enemy4Animation>();
            enemy4Attack1.enabled = true;
            enemy4Animation.SetLookingDirection(ObjectMove.GetLookingAtPlayerDirection(objectStateMachine.transform.position));
            enemy4Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy4CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy4DeadState());
            else if(!enemy4Attack1.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy4Move2State());
        }

        public void Exit()
        {
            enemy4Attack1.StopAttack();
            enemy4Attack1.enabled = false;
            enemy4Animation.StopAttack();
        }
    }
}
