using Assets.Scripts.Battle;
using Assets.Scripts.Objects;
using Assets.Scripts.Sounds;
using Assets.Scripts.Stage;
using Assets.Scripts.UI;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Room
{
    public class BattleRoomState : IRoomState
    {
        private RoomStateMachine roomStateMachine;
        private bool isSet = false;

        public async UniTask Enter(RoomStateMachine roomStateMachine)
        {
            this.roomStateMachine = roomStateMachine;
            await StageFacade.CreateBattleStage();
            await ObjectFacade.CreateBattleObjects();
            PlayBGM.SingletonInstance.PlayBattleRoom();
            isSet = true;
        }

        public async void FixedUpdate()
        {
            if (!isSet)
                return;
            if (!ObjectFacade.IsPlayerLiving())
                await roomStateMachine.TransitionTo(new LobbyState());
            else if (!ObjectFacade.IsEnemyLiving())
            {
                BattleFacade.AddStageCount();
                await roomStateMachine.TransitionTo(new BattleRoomState());
            }
            else if(PoseBackerToLobby.IsBackingToLobby)
            {
                PoseBackerToLobby.IsBackingToLobby = false;
                BattleFacade.ResetData();
                await roomStateMachine.TransitionTo(new LobbyState());
            }
        }

        public void Exit()
        {
            ObjectFacade.CleanAllObjects();
        }
    }
}
