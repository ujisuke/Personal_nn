using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy2DeadState : IObjectState
    {
        private Enemy2Dead enemy2Dead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            enemy2Dead = objectStateMachine.GetComponent<Enemy2Dead>();
            enemy2Dead.enabled = true;
            objectStateMachine.GetComponent<Enemy2Animation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
