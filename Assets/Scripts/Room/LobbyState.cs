using Assets.Scripts.Stage;
using Assets.Scripts.Objects;
using Cysharp.Threading.Tasks;
using Assets.Scripts.Sounds;
using Assets.Scripts.UI;

namespace Assets.Scripts.Room
{
    public class LobbyState : IRoomState
    {
        private RoomStateMachine roomStateMachine;
        private bool isSet = false;
        public static bool IsBackingToLobby = false;

        public async UniTask Enter(RoomStateMachine roomStateMachine)
        {
            this.roomStateMachine = roomStateMachine;
            IsBackingToLobby = false;
            PoseMain.IsInLobby = true;
            CanvasStorage.SingletonInstance.ActivateCanvases();
            await StageCreator.SingletonInstance.CreateLobbyStage();
            await ObjectFacade.CreateLobbyObjects();
            ObjectStorage.IsStartBattleEnemyLiving = true;
            ObjectStorage.IsSetGameEnemyLiving = true;
            BGMPlayer.SingletonInstance.PlayLobby();
            isSet = true;
        }

        public async void FixedUpdate()
        {
            if (!isSet)
                return;
            if(!ObjectFacade.IsStartBattleEnemyLiving())
                await roomStateMachine.TransitionTo(new BattleRoomState());
        }

        public void Exit()
        {
            ObjectFacade.CleanAllObjects();
            PoseMain.IsInLobby = false;
        }
    }
}
