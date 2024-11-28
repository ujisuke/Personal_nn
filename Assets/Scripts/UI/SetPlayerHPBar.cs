using UnityEngine.UI;
using UnityEngine;
using Assets.ScriptableObjects;
using Assets.Scripts.Player;
using Assets.Scripts.Objects;

namespace Assets.Scripts.UI
{
    public class SetPlayerHPBar : MonoBehaviour
    {
        [SerializeField] private GameObject _SeparateUIPrefab;
        [SerializeField] private PlayerParameter _playerParameter;
        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();
            float barWidth = GetComponent<RectTransform>().rect.width;
            for(int i = 0 ; i < _playerParameter.MaxHP + 1; i++)
            {
                GameObject newSeparateUIPrefab = Instantiate(_SeparateUIPrefab, transform.position, Quaternion.identity);
                newSeparateUIPrefab.transform.SetParent(transform);
                newSeparateUIPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(barWidth * (i / (float)_playerParameter.MaxHP - 0.5f), 0, 0);
            }
        }

        private void FixedUpdate()
        {
            if(!ObjectFacade.IsPlayerLiving()) return;
            image.fillAmount = PlayerMain.CurrentHP / (float)_playerParameter.MaxHP;
        }
    }
}
