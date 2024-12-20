using Assets.Scripts.Battle;
using Assets.Scripts.Objects;
using Assets.Scripts.Sounds;
using Assets.Scripts.Stage;
using Assets.Scripts.UI;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Room
{
    public class ClearRoomState : IRoomState
    {
        private RoomStateMachine roomStateMachine;

        public async UniTask Enter(RoomStateMachine roomStateMachine)
        {
            this.roomStateMachine = roomStateMachine;
            BattleData.Clear();
            await StageCreator.SingletonInstance.CreateClearStage();
            await ObjectFacade.CreateClearObjects();
            BGMPlayer.SingletonInstance.PlayClearRoom();
            ClearText.Display().SuppressCancellationThrow().Forget();
        }

        public async void FixedUpdate()
        {
            if(LobbyState.IsBackingToLobby)
            {
                BattleData.AddStageCount();
                await roomStateMachine.TransitionTo(new LobbyState());
            }
        }

        public void Exit()
        {
            ClearText.Hide();
            ClearIcon.Display();
            ObjectFacade.CleanAllObjects();
        }
    }
}
