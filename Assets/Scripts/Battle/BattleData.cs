using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class BattleData : MonoBehaviour
    {
        private static readonly int _stageDifficultySpan = 5;
        public static int StageDifficulty => (stageCount - 1) / _stageDifficultySpan + 1;
        private static readonly int _enemyDifficultySpan = 2;
        public static int EnemyDifficulty => (stageCount - 1) / _enemyDifficultySpan + 1;
        private static int stageCount = 1;
        public static int StageCount => stageCount;
        private static int deathCount = 0;
        public static int DeathCount => deathCount;

        public static void AddStageCount()
        {
            stageCount++;
        }

        public static void AddDeathCount()
        {
            deathCount++;
        }

        public static void Reset()
        {
            stageCount = 1;
            deathCount = 0;
        }
    }
}
