using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PoseGameExiter : MonoBehaviour
    {
        private void OnEnable()
        {
            Application.Quit();
        }
    }
}
