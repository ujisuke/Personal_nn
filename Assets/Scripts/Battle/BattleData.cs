using UnityEngine;
using Unity.Mathematics;

namespace Assets.Scripts.Battle
{
    public class BattleData : MonoBehaviour
    {
        private static readonly int _stageDifficultySpan = 5;
        public static int StageDifficulty => (StageCount - 1) / _stageDifficultySpan + 1;
        private static readonly int _enemyDifficultySpan = 2;
        public static int EnemyDifficulty => (StageCount - 1) / _enemyDifficultySpan + 1;
        public static int StageCount = 1;
        public static int DeathCount = 0;
    }
}
