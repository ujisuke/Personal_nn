using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerCleanedState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            ObjectStorage.RemovePlayer();
            objectStateMachine.GetComponent<ObjectCleaned>().enabled = true;
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
