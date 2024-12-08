using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.UIEnemies
{
    public class BackToLobbyEvent : MonoBehaviour, IEvent
    {
        public void Execute()
        {
            ObjectStorage.IsBackToLobbyEnemyLiving = false;
        }
    }
}
