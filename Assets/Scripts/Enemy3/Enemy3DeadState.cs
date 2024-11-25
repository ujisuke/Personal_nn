using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3DeadState : IObjectState
    {
        private Enemy3Dead enemy3Dead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            enemy3Dead = objectStateMachine.GetComponent<Enemy3Dead>();
            enemy3Dead.enabled = true;
            objectStateMachine.GetComponent<Enemy3Animation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
