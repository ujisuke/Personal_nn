using UnityEngine;
using Assets.Scripts.Battle;

namespace Assets.Scripts.UIEnemies
{
    public class ExitGameEvent : MonoBehaviour, IEvent
    {
        public void Execute()
        {
            BattleData.Save();
            Application.Quit();
        }
    }
}
