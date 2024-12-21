using UnityEngine.UI;
using UnityEngine;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.UI
{
    public class PlayerHPBar : MonoBehaviour
    {
        [SerializeField] private GameObject _SeparateUIPrefab;
        [SerializeField] private PlayerParameter _playerParameter;
        private Image image;
        private static PlayerHPBar singletonInstance;
        public static PlayerHPBar SingletonInstance => singletonInstance;

        private void Awake()
        {
            image = GetComponent<Image>();
            float barWidth = GetComponent<RectTransform>().rect.width;
            for(int i = 0 ; i < _playerParameter.MaxHP + 1; i++)
            {
                GameObject newSeparateUIPrefab = Instantiate(_SeparateUIPrefab, transform.position, Quaternion.identity);
                newSeparateUIPrefab.transform.SetParent(transform);
                newSeparateUIPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(barWidth * (i / (float)_playerParameter.MaxHP - 0.5f), 0, 0);
            }
            singletonInstance = this;
        }

        public void TakeDamage(int hPValue)
        {
            image.fillAmount = (float)hPValue / _playerParameter.MaxHP;
            Flash().Forget();
        }

        private async UniTask Flash()
        {
            for(int i = 0; i < 3; i++)
            {
                image.enabled = false;
                await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.InvincibleTime / 6), ignoreTimeScale: true);
                image.enabled = true;
                await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.InvincibleTime / 6), ignoreTimeScale: true);
            }
        }

        public void ResetValue()
        {
            image.fillAmount = 1;
        }
    }
}
