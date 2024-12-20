using Assets.Scripts.Room;
using UnityEngine;

namespace Assets.Scripts.UIEnemies
{
    public class BackToLobbyEvent : MonoBehaviour, IEvent
    {
        public void Execute()
        {
            LobbyState.IsBackingToLobby = true;
        }
    }
}
