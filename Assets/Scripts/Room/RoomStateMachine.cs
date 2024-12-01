using UnityEngine;

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

        public void TransitionTo(IRoomState newState)
        {
            currentState.Exit();
            currentState = newState;
            newState.Enter(this);
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
    }
}