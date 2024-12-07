using Cysharp.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Room
{
    public class RoomStateMachine : MonoBehaviour
    {
        private IRoomState currentState;

        private void Awake()
        {
            currentState = new LobbyState();
            currentState.Enter(this);
        }

        public async UniTask TransitionTo(IRoomState newState)
        {
            currentState.Exit();
            currentState = newState;
            await UniTask.WaitUntil(() => !ObjectFacade.IsPlayerLiving() && !ObjectFacade.IsEnemyLiving());
            await newState.Enter(this);
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
    }
}