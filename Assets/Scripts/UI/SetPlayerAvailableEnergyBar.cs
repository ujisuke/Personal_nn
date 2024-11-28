using UnityEngine.UI;
using UnityEngine;
using Assets.ScriptableObjects;
using Assets.Scripts.Player;
using Assets.Scripts.Objects;

namespace Assets.Scripts.UI
{
    public class SetPlayerAvailableEnergyBar : MonoBehaviour
    {
        [SerializeField] private GameObject _separateUIPrefab;
        [SerializeField] private PlayerParameter _playerParameter;
        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();
            float barWidth = GetComponent<RectTransform>().rect.width;
            for(int i = 0 ; i < _playerParameter.MaxEnergy + 1; i++)
            {
                GameObject newSeparateUIPrefab = Instantiate(_separateUIPrefab, transform.position, Quaternion.identity);
                newSeparateUIPrefab.transform.SetParent(transform);
                newSeparateUIPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(barWidth * (i / (float)_playerParameter.MaxEnergy - 0.5f), 0, 0);
            }
        }

        private void FixedUpdate()
        {
            if(!ObjectFacade.IsPlayerLiving()) return;
            image.fillAmount = PlayerMain.CurrentAvailableEnergy / (float)_playerParameter.MaxEnergy;
        }
    }
}
