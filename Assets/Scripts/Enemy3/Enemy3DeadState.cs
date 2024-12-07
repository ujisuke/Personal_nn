using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3DeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            objectStateMachine.GetComponent<Enemy3Dead>().enabled = true;
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
