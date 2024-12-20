using System;
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
        private static int seed = 0;
        public static int Seed => seed;
        public static int BattleSeed => seed + stageCount;
        private static readonly int _clearStageCount = 30;
        private static int clearFlag = 0;

        private void Awake()
        {
            stageCount = PlayerPrefs.GetInt("StageCount", 1);
            deathCount = PlayerPrefs.GetInt("DeathCount", 0);
            seed = PlayerPrefs.GetInt("Seed", DateTime.Now.Millisecond);
            clearFlag = PlayerPrefs.GetInt("ClearFlag", 0);
        }

        public static void Save()
        {
            PlayerPrefs.SetInt("StageCount", stageCount);
            PlayerPrefs.SetInt("DeathCount", deathCount);
            PlayerPrefs.SetInt("Seed", seed);
            PlayerPrefs.SetInt("ClearFlag", clearFlag);
        }

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
            seed = DateTime.Now.Millisecond;
        }

        public static void ReplaceSeed(int inputSeed)
        {
            seed = inputSeed;
        }

        public static void Clear()
        {
            clearFlag = 1;
        }

        public static bool IsClearedNow()
        {
            return stageCount == _clearStageCount;
        }

        public static bool IsClearedBefore()
        {
            return clearFlag == 1;
        }
    }
}
