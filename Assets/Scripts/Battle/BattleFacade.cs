using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class BattleFacade : MonoBehaviour
    {
        public static int StageCount => BattleData.StageCount;
        public static int DeathCount => BattleData.DeathCount;
        public static int StageDifficulty => BattleData.StageDifficulty;
        public static int EnemyDifficulty => BattleData.EnemyDifficulty;

        public static void AddStageCount()
        {
            BattleData.AddStageCount();
        }

        public static void AddDeathCount()
        {
            BattleData.AddDeathCount();
        }

        public static void ResetData()
        {
            BattleData.Reset();
        }
    }
}