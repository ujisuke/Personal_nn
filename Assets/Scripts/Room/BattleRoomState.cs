using Assets.Scripts.Battle;
using Assets.Scripts.Objects;
using Assets.Scripts.Sounds;
using Assets.Scripts.Stage;
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
            await StageCreator.SingletonInstance.CreateBattleStage();
            await ObjectFacade.CreateBattleObjects();
            BGMPlayer.SingletonInstance.PlayBattleRoom();
            isSet = true;
        }

        public async void FixedUpdate()
        {
            if (!isSet)
                return;
            if (!ObjectFacade.IsPlayerLiving())
                await roomStateMachine.TransitionTo(new BattleRoomState());
            else if (!ObjectFacade.IsEnemyLiving())
            {
                if(BattleData.IsClearedNow())
                    await roomStateMachine.TransitionTo(new ClearRoomState());
                else
                {
                    BattleData.AddStageCount();
                    await roomStateMachine.TransitionTo(new BattleRoomState());
                }
            }
            else if(LobbyState.IsBackingToLobby)
                await roomStateMachine.TransitionTo(new LobbyState());
        }

        public void Exit()
        {
            ObjectFacade.CleanAllObjects();
        }
    }
}
