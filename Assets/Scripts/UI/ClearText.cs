using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ClearText : MonoBehaviour
    {
        private static Text text;
        private static Color32 _textColor;
        private static CancellationTokenSource cancellationTokenSource = null;

        private void Awake()
        {
            text = GetComponentInChildren<Text>();
            _textColor = text.color;
            Hide();
        }

        public static async UniTask Display()
        {
            cancellationTokenSource = new();
            text.enabled = true;
            FadeIn().Forget();
            Up().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken : cancellationTokenSource.Token);
            await FadeOut();
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken : cancellationTokenSource.Token);
        }

        private static async UniTask FadeIn()
        {
            for(int i = 1; i <= 8; i++)
            {
                text.color = new Color32(_textColor.r, _textColor.g, _textColor.b, (byte)(i * 32 - 1));
                await UniTask.Delay(TimeSpan.FromSeconds(0.06f), cancellationToken : cancellationTokenSource.Token);
            }
        }

        private static async UniTask FadeOut()
        {
            for(int i = 7; i >= 0; i--)
            {
                text.color = new Color32(_textColor.r, _textColor.g, _textColor.b, (byte)(i * 32));
                await UniTask.Delay(TimeSpan.FromSeconds(0.06f), cancellationToken : cancellationTokenSource.Token);
            }
        }

        private static async UniTask Up()
        {
            float textX = text.rectTransform.anchoredPosition.x;
            for(int i = 0; i < 8; i++)
            {
                text.rectTransform.anchoredPosition = new Vector2(textX, i);
                await UniTask.Delay(TimeSpan.FromSeconds(0.06f), cancellationToken : cancellationTokenSource.Token);
            }
        }

        public static void Hide()
        {
            cancellationTokenSource?.Cancel();
            text.enabled = false;
        }
    }
}
