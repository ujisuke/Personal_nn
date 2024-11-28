using UnityEngine;
using Unity.Mathematics;

namespace Assets.Scripts.Battle
{
    public class BattleData : MonoBehaviour
    {
        private static int difficulty = 1;
        public static int Difficulty => difficulty;
        private static int stageCount = 1;
        public static int StageCount => stageCount;
        public static int DeathCount = 0;
        private static readonly int _maxDifficulty = 3;

        public static void GoToNextStage()
        {
            stageCount++;
            difficulty = math.min(stageCount / 2 + 1, _maxDifficulty);
        }
    }
}
