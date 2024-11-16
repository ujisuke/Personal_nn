using System;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1AttackState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy1Attack enemy1Attack;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy1Attack = objectStateMachine.GetComponent<Enemy1Attack>();
            enemy1Attack.enabled = true;
        }

        public void FixedUpdate()
        {
            if(!enemy1Attack.IsAttacking)
                objectStateMachine.TransitionTo(new Enemy1MoveState());
        }

        public void Exit()
        {
            enemy1Attack.enabled = false;
        }
    }
}
