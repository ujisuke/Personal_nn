using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3AttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy3Attack enemy3Attack;
        private Enemy3Main enemy3Main;
        private Enemy3Animation enemy3Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy3Attack = objectStateMachine.GetComponent<Enemy3Attack>();
            enemy3Main = objectStateMachine.GetComponent<Enemy3Main>();
            enemy3Animation = objectStateMachine.GetComponent<Enemy3Animation>();
            enemy3Attack.enabled = true;
            enemy3Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemy3Main.IsDead())
                objectStateMachine.TransitionTo(new Enemy3DeadState());
            if(!enemy3Attack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy3MoveState());
        }

        public void Exit()
        {
            enemy3Attack.enabled = false;
            enemy3Animation.StopAttack();
        }
    }
}