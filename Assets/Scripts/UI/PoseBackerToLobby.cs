using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PoseBackerToLobby : MonoBehaviour
    {
        public static bool IsBackingToLobby = false;
        
        private void OnEnable()
        {
            IsBackingToLobby = true;
        }
    }
}
