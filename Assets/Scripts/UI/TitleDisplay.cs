using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TitleDisplay : MonoBehaviour
    {
        private Image image;
        private Text text;
        private bool isDisplaying = true;
        public bool IsDisplaying => isDisplaying;
        private static TitleDisplay singletonInstance;
        public static TitleDisplay SingletonInstance => singletonInstance;

        private void Awake()
        {
            singletonInstance = this;
            image = GetComponentInChildren<Image>();
            text = GetComponentInChildren<Text>();
            isDisplaying = true;
        }

        public async void Display()
        {
            FadeIn();
            Up();
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            await FadeOut();
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            isDisplaying = false;
        }

        private async void FadeIn()
        {
            for(int i = 1; i <= 8; i++)
            {
                image.color = new Color32(255, 255, 255, (byte)(i * 32 - 1));
                text.color = new Color32(255, 255, 255, (byte)(i * 32 - 1) );
                await UniTask.Delay(TimeSpan.FromSeconds(0.06f));
            }
        }

        private async UniTask FadeOut()
        {
            for(int i = 7; i >= 0; i--)
            {
                image.color = new Color32(255, 255, 255, (byte)(i * 32));
                text.color = new Color32(255, 255, 255, (byte)(i * 32));
                await UniTask.Delay(TimeSpan.FromSeconds(0.06f));
            }
        }

        private async void Up()
        {
            for(int i = 0; i < 8; i++)
            {
                image.rectTransform.anchoredPosition = new Vector2(0, i);
                text.rectTransform.anchoredPosition = new Vector2(0, i);
                await UniTask.Delay(TimeSpan.FromSeconds(0.06f));
            }
        }
    }
}
