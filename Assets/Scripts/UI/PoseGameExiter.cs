using UnityEngine;
using Assets.Scripts.Battle;

namespace Assets.Scripts.UI
{
    public class PoseGameExiter : MonoBehaviour
    {
        private void OnEnable()
        {
            BattleData.Save();
            Application.Quit();
        }
    }
}
