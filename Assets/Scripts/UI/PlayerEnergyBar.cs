using UnityEngine.UI;
using UnityEngine;
using Assets.ScriptableObjects;

namespace Assets.Scripts.UI
{
    public class PlayerEnergyBar : MonoBehaviour
    {
        [SerializeField] private GameObject _separateUIPrefab;
        [SerializeField] private PlayerParameter _playerParameter;
        private Image image;
        private static PlayerEnergyBar singletonInstance;
        public static PlayerEnergyBar SingletonInstance => singletonInstance;

        private void Awake()
        {
            image = GetComponent<Image>();
            float barWidth = GetComponent<RectTransform>().rect.width;
            for(int i = 0 ; i < _playerParameter.MaxEnergy + 1; i++)
            {
                GameObject newSeparateUIPrefab = Instantiate(_separateUIPrefab, transform.position, Quaternion.identity);
                newSeparateUIPrefab.transform.SetParent(transform);
                newSeparateUIPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(barWidth * (i / (float)_playerParameter.MaxEnergy - 0.5f), 0, 0);
            }
            singletonInstance = this;
        }

        public void SetValue(float energyValue)
        {
            image.fillAmount = energyValue / _playerParameter.MaxEnergy;
        }

        public void ResetValue()
        {
            image.fillAmount = 1;
        }
    }
}
