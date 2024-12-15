using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CanvasStorage : MonoBehaviour
    {
        [SerializeField] private Canvas descriptionCanvas;
        private static Canvas singletonDescriptionCanvas;
        public static Canvas SingletonDescriptionCanvas => singletonDescriptionCanvas;
        [SerializeField] private Canvas poseCanvas;
        private static Canvas singletonPoseCanvas;
        private bool isEscapePushed = false;

        private void Awake()
        {
            singletonDescriptionCanvas = descriptionCanvas;
            singletonPoseCanvas = poseCanvas;
        }

        private void Update()
        {
            if(isEscapePushed && !Input.GetKey(KeyCode.Escape))
                isEscapePushed = false;
            if(isEscapePushed)
                return;
            if (Input.GetKey(KeyCode.Escape))
            {
                isEscapePushed = true;
                if(singletonPoseCanvas.gameObject.activeSelf)
                    HidePoseCanvas();
                else
                    ShowPoseCanvas();
            }
        }

        public static void ShowPoseCanvas()
        {
            singletonPoseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public static void HidePoseCanvas()
        {
            singletonPoseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}