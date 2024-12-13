using UnityEngine.UI;
using UnityEngine;
using Assets.ScriptableObjects;

namespace Assets.Scripts.UI
{
    public class SetPlayerAvailableEnergyBar : MonoBehaviour
    {
        [SerializeField] private GameObject _separateUIPrefab;
        [SerializeField] private PlayerParameter _playerParameter;
        private Image image;
        public static SetPlayerAvailableEnergyBar _SingletonInstance;

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
            _SingletonInstance = this;
        }

        public void SetValue(int energyValue)
        {
            image.fillAmount = (float)energyValue / _playerParameter.MaxEnergy;
        }

        public void ResetValue()
        {
            image.fillAmount = 1;
        }
    }
}
