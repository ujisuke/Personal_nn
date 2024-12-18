using Assets.Scripts.Battle;
using UnityEngine;

namespace Assets.Scripts.UIEnemies
{
    public class ResetDataEvent : MonoBehaviour, IEvent
    {
        public void Execute()
        {
            BattleData.Reset();
        }
    }
}
