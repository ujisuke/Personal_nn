using Assets.Scripts.Battle;
using Assets.Scripts.Objects;
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
            await StageFacade.CreateBattleStage();
            await ObjectFacade.CreateBattleObjects();
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
        }

        public void Exit()
        {
            ObjectFacade.KillAllObjects();
        }
    }
}
