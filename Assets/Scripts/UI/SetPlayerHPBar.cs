using UnityEngine.UI;
using UnityEngine;
using Assets.ScriptableObjects;
using Assets.Scripts.Player;
using Assets.Scripts.Objects;
using System.Collections;

namespace Assets.Scripts.UI
{
    public class SetPlayerHPBar : MonoBehaviour
    {
        [SerializeField] private GameObject _SeparateUIPrefab;
        [SerializeField] private PlayerParameter _playerParameter;
        private Image image;
        private int latestCurrentHP;

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
            if(latestCurrentHP != PlayerMain.CurrentHP && PlayerMain.CurrentHP != _playerParameter.MaxHP) StartCoroutine(Flash());
            latestCurrentHP = PlayerMain.CurrentHP;
        }

        private IEnumerator Flash()
        {
            for(int i = 0; i < 3; i++)
            {
                image.enabled = false;
                yield return new WaitForSeconds(_playerParameter.InvincibleTime / 6);
                image.enabled = true;
                yield return new WaitForSeconds(_playerParameter.InvincibleTime / 6);
            }
        }
    }
}
