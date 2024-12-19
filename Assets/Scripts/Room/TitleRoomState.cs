using Cysharp.Threading.Tasks;
using Assets.Scripts.UI;

namespace Assets.Scripts.Room
{
    public class TitleRoomState : IRoomState
    {
        private RoomStateMachine roomStateMachine;

        public async UniTask Enter(RoomStateMachine roomStateMachine)
        {
            this.roomStateMachine = roomStateMachine;
            await UniTask.WaitUntil(() => CanvasStorage.SingletonInstance != null);
            CanvasStorage.SingletonInstance.IsInTitleRoom = true;
            TitleDisplay.SingletonInstance.Display();
        }

        public async void FixedUpdate()
        {
            if(!TitleDisplay.SingletonInstance.IsDisplaying)
                await roomStateMachine.TransitionTo(new LobbyState());
        }

        public void Exit()
        {
            CanvasStorage.SingletonInstance.IsInTitleRoom = false;
        }
    }
}
