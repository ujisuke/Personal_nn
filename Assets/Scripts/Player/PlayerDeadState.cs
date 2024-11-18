using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerDeadState : IObjectState
    {
        private PlayerDead playerDead;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            playerDead = objectStateMachine.GetComponent<PlayerDead>();
            playerDead.enabled = true;
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
