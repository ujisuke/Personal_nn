using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1DeadState : IObjectState
    {
        private Enemy1Dead enemy1Dead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            enemy1Dead = objectStateMachine.GetComponent<Enemy1Dead>();
            enemy1Dead.enabled = true;
            objectStateMachine.GetComponent<Enemy1Animation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
