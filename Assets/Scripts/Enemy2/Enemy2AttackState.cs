using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2AttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy2Attack enemy2Attack;
        private Enemy2Main enemy2Main;
        private Enemy2Animation enemy2Animation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy2Attack = objectStateMachine.GetComponent<Enemy2Attack>();
            enemy2Main = objectStateMachine.GetComponent<Enemy2Main>();
            enemy2Animation = objectStateMachine.GetComponent<Enemy2Animation>();
            enemy2Attack.enabled = true;
            enemy2Animation.SetLookingDirection(enemy2Attack.GetLookingDirection());
            enemy2Animation.StartAttack();
        }

        public void FixedUpdate()
        {
            if(enemy2Main.IsDead())
                objectStateMachine.TransitionTo(new Enemy2DeadState());
            if(!enemy2Attack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy2MoveState());
        }

        public void Exit()
        {
            enemy2Attack.enabled = false;
            enemy2Animation.StopAttack();
        }
    }
}
