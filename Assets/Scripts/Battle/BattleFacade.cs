using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class BattleFacade : MonoBehaviour
    {
        public static int Difficulty => BattleData.Difficulty;
        public static int StageCount => BattleData.StageCount;
        public static int DeathCount => BattleData.DeathCount;
        private static BattleSetter singletonBattleSetter;

        private void Awake()
        {
            singletonBattleSetter = GetComponent<BattleSetter>();
        }

        public static void ResetStage()
        {
            singletonBattleSetter.ResetBattle();
        }

        public static void AddDeathCount()
        {
            BattleData.DeathCount++;
        }
    }
}