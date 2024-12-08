using Assets.Scripts.Battle;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerDeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            ObjectStorage.RemovePlayer();
            objectStateMachine.GetComponent<ObjectDead>().enabled = true;
            objectStateMachine.GetComponent<PlayerAnimation>().StartDead();
            BattleFacade.AddDeathCount();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
