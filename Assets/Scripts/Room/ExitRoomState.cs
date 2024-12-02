using Assets.Scripts.Stage;
using Assets.Scripts.Battle;
using Assets.Scripts.Objects;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Room
{
    public class ExitRoomState : IRoomState
    {
        public UniTask Enter(RoomStateMachine roomStateMachine)
        {
            Application.Quit();
            return UniTask.CompletedTask;
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}

