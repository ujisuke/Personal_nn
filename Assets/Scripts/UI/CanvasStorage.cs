using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CanvasStorage : MonoBehaviour
    {
        [SerializeField] private Canvas descriptionCanvas;
        public Canvas DescriptionCanvas => descriptionCanvas;
        [SerializeField] private Canvas poseCanvas;
        [SerializeField] private Canvas manualCanvas;
        [SerializeField] private Canvas uiCanvas;
        private bool isEscapePushed = false;
        public bool IsInTitleRoom = true;
        private static CanvasStorage singletonInstance;
        public static CanvasStorage SingletonInstance => singletonInstance;
    
        private void Awake()
        {
            singletonInstance = this;
        }

        private void Update()
        {
            if(isEscapePushed && !Input.GetKey(KeyCode.Escape))
                isEscapePushed = false;
            if(isEscapePushed)
                return;
            if (Input.GetKey(KeyCode.Escape) && !IsInTitleRoom)
            {
                isEscapePushed = true;
                if(poseCanvas.gameObject.activeSelf)
                    HidePoseCanvas();
                else
                    ShowPoseCanvas();
            }
        }

        public void ShowPoseCanvas()
        {
            poseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void HidePoseCanvas()
        {
            poseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void ShowCanvases()
        {
            uiCanvas.gameObject.SetActive(true);
            manualCanvas.gameObject.SetActive(true);
            descriptionCanvas.gameObject.SetActive(true);
        }
    }
}