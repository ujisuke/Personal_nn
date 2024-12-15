using Assets.Scripts.Stage;
using Assets.Scripts.Objects;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Room
{
    public class SettingRoomState : IRoomState
    {
        public async UniTask Enter(RoomStateMachine roomStateMachine)
        {
            await StageFacade.CreateSettingStage();
            await ObjectFacade.CreateSettingObjects();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            ObjectFacade.CleanAllObjects();
        }
    }
}
