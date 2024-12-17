using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Battle;

namespace Assets.Scripts.UI
{
    public class StageCountAndDeathCountDisplay : MonoBehaviour
    {
        [SerializeField] private int textFontSize = 0;
        [SerializeField] private int countFontSize = 0;
        private Text text;

        private void Start()
        {
            text = GetComponent<Text>();
        }

        private void FixedUpdate()
        {
            text.text = $"<size={textFontSize}>ステージ</size><size={countFontSize}>{BattleFacade.StageCount}</size><size={textFontSize}> デス</size><size={countFontSize}>{BattleFacade.DeathCount}</size>";
        }
    }
}
