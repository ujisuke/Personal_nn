using System;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1MoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private Enemy1Move enemy1Move;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            enemy1Move = objectStateMachine.GetComponent<Enemy1Move>();
            enemy1Move.enabled = true;
        }

        public void FixedUpdate()
        {
            if(enemy1Move.CanAttack())
                objectStateMachine.TransitionTo(new Enemy1AttackState());
        }

        public void Exit()
        {
            enemy1Move.enabled = false;
        }
    }
}
