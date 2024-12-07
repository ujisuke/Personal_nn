using Assets.Scripts.Stage;
using Assets.Scripts.Objects;
using Cysharp.Threading.Tasks;

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
            isSet = true;
        }

        public async void FixedUpdate()
        {
            if (!isSet)
                return;
            if(!ObjectFacade.IsStartBattleEnemyLiving())
                await roomStateMachine.TransitionTo(new BattleRoomState());
            else if(!ObjectFacade.IsSetGameEnemyLiving())
                await roomStateMachine.TransitionTo(new SettingRoomState());
        }

        public void Exit()
        {
            ObjectFacade.KillAllObjects();
        }
    }
}
