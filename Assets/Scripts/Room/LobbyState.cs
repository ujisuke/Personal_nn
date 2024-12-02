using Assets.Scripts.Stage;
using Assets.Scripts.Battle;
using Assets.Scripts.Objects;
using System.Collections;
using UnityEngine;
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

        public void FixedUpdate()
        {
            if (!isSet)
                return;
            if (!ObjectFacade.IsExitEnemyLiving())
                roomStateMachine.TransitionTo(new ExitRoomState());
            else if(!ObjectFacade.IsStartBattleEnemyLiving())
                roomStateMachine.TransitionTo(new BattleRoomState());
        }

        public void Exit()
        {
            ObjectFacade.ClearObjects();
        }
    }
}
