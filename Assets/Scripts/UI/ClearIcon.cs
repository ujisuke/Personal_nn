using Assets.Scripts.Battle;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ClearIcon : MonoBehaviour
    {
        private static Text text;

        private void Start()
        {
            text = GetComponent<Text>();
            Hide();
            if(BattleData.IsClearedBefore())
                Display();
        }

        public static void Hide()
        {
            text.enabled = false;
        }

        public static void Display()
        {
            text.enabled = true;
        }
    }
}
