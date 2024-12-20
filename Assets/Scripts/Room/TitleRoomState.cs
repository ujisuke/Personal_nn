using Cysharp.Threading.Tasks;
using Assets.Scripts.UI;

namespace Assets.Scripts.Room
{
    public class TitleRoomState : IRoomState
    {
        private RoomStateMachine roomStateMachine;
        private bool isSet = false;

        public async UniTask Enter(RoomStateMachine roomStateMachine)
        {
            isSet = false;
            this.roomStateMachine = roomStateMachine;
            await TitleDisplay.Display();
            isSet = true;
        }

        public async void FixedUpdate()
        {
            if(isSet)
                await roomStateMachine.TransitionTo(new LobbyState());
        }

        public void Exit()
        {

        }
    }
}
