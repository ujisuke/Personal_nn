using Cysharp.Threading.Tasks;
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
            BattleData.StageCount++;
        }

        public static void AddDeathCount()
        {
            BattleData.DeathCount++;
        }
    }
}