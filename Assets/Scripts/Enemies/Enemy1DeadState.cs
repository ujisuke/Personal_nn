using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1DeadState : IObjectState
    {
        private Enemy1Dead enemy1Dead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            enemy1Dead = objectStateMachine.GetComponent<Enemy1Dead>();
            enemy1Dead.enabled = true;
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
