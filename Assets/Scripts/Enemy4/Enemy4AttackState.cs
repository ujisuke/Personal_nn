using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4AttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy4Attack enemy4Attack;
        private EnemyMain enemyMain;
        private Enemy4Animation enemy4Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy4Attack = objectStateMachine.GetComponent<Enemy4Attack>();
            enemyMain = objectStateMachine.GetComponent<EnemyMain>();
            enemy4Animation = objectStateMachine.GetComponent<Enemy4Animation>();
            enemy4Attack.enabled = true;
            enemy4Animation.SetLookingDirection(ObjectMove.GetLookingAtPlayerDirection(objectStateMachine.transform.position));
            enemy4Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemyMain.IsCleaned)
                objectStateMachine.TransitionTo(new Enemy4CleanedState());
            else if(enemyMain.IsDead())
                objectStateMachine.TransitionTo(new Enemy4DeadState());
            else if(!enemy4Attack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy4MoveState());
        }

        public void Exit()
        {
            enemy4Attack.StopAttack();
            enemy4Attack.enabled = false;
            enemy4Animation.StopAttack();
        }
    }
}
