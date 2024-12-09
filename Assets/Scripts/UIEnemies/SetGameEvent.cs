using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.UIEnemies
{
    public class SetGameEvent : MonoBehaviour, IEvent
    {
        public void Execute()
        {
            ObjectStorage.IsSetGameEnemyLiving = false;
        }
    }
}
