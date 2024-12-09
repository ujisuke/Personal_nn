using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CanvasStorage : MonoBehaviour
    {
        private static Canvas singletonDescriptionCanvas;
        public static Canvas SingletonDescriptionCanvas => singletonDescriptionCanvas;

        private void Awake()
        {
            singletonDescriptionCanvas = GetComponent<Canvas>();
        }
    }
}