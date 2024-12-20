using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TitleDisplay : MonoBehaviour
    {
        private static Image image;
        private static Text text;
        private static Color32 _imageColor;
        private static Color32 _textColor;

        private void Awake()
        {
            image = GetComponentInChildren<Image>();
            _imageColor = image.color;
            text = GetComponentInChildren<Text>();
            _textColor = text.color;
        }

        public static async UniTask Display()
        {
            FadeIn().Forget();
            Up().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            await FadeOut();
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        }

        private static async UniTask FadeIn()
        {
            for(int i = 1; i <= 8; i++)
            {
                image.color = new Color32(_imageColor.r, _imageColor.g, _imageColor.b, (byte)(i * 32 - 1));
                text.color = new Color32(_textColor.r, _textColor.g, _textColor.b, (byte)(i * 32 - 1));
                await UniTask.Delay(TimeSpan.FromSeconds(0.06f));
            }
        }

        private static async UniTask FadeOut()
        {
            for(int i = 7; i >= 0; i--)
            {
                image.color = new Color32(_imageColor.r, _imageColor.g, _imageColor.b, (byte)(i * 32));
                text.color = new Color32(_textColor.r, _textColor.g, _textColor.b, (byte)(i * 32));
                await UniTask.Delay(TimeSpan.FromSeconds(0.06f));
            }
        }

        private static async UniTask Up()
        {
            float imageX = image.rectTransform.anchoredPosition.x;
            float textX = text.rectTransform.anchoredPosition.x;
            for(int i = 0; i < 8; i++)
            {
                image.rectTransform.anchoredPosition = new Vector2(imageX, i);
                text.rectTransform.anchoredPosition = new Vector2(textX, i);
                await UniTask.Delay(TimeSpan.FromSeconds(0.06f));
            }
        }
    }
}
