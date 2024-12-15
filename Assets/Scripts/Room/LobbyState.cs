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

        public async UniTask Enter(RoomStateMachine roomStateMachine)
        {
            this.roomStateMachine = roomStateMachine;
            await StageFacade.CreateLobbyStage();
            await ObjectFacade.CreateLobbyObjects();
            ObjectStorage.IsStartBattleEnemyLiving = true;
            ObjectStorage.IsSetGameEnemyLiving = true;
            PoseMain.IsInLobby = true;
            PlayBGM.SingletonInstance.PlayLobby();
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
