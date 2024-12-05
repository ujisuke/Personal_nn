using Assets.Scripts.Stage;
using Assets.Scripts.Battle;
using Assets.Scripts.Objects;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Room
{
    public class SettingRoomState : IRoomState
    {
        private RoomStateMachine roomStateMachine;
        private bool isSet = false;

        public async UniTask Enter(RoomStateMachine roomStateMachine)
        {
            this.roomStateMachine = roomStateMachine;
            await StageFacade.CreateSettingStage();
            await ObjectFacade.CreateSettingObjects();
            isSet = true;
        }

        public void FixedUpdate()
        {
            if (!isSet)
                return;
            if (!ObjectFacade.IsBackToLobbyEnemyLiving())
                roomStateMachine.TransitionTo(new LobbyState());
        }

        public void Exit()
        {
            ObjectFacade.ClearObjects();
        }
    }
}
