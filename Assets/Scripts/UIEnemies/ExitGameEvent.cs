using UnityEngine;

namespace Assets.Scripts.UIEnemies
{
    public class ExitGameEvent : MonoBehaviour, IEvent
    {
        public void Execute()
        {
            Application.Quit();
        }
    }
}
