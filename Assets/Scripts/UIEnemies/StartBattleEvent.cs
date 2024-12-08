using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.UIEnemies
{
    public class StartBattleEvent : MonoBehaviour, IEvent
    {
        public void Execute()
        {
            ObjectStorage.IsStartBattleEnemyLiving = false;
        }
    }
}
