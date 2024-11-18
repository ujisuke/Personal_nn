using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Unity.Mathematics;

namespace Assets.Scripts.Battle
{
    public class BattleFacade : MonoBehaviour
    {
        private static int difficulty = 1;
        public static int Difficulty => difficulty;
        private static int stageNumber = 1;
        private static readonly int _maxDifficulty = 3;

        private void Start()
        {
            ResetBattle();
        }

        private void FixedUpdate()
        {
            if(ObjectFacade.IsEnemyLiving()) return;
            stageNumber++;
            difficulty = math.min(stageNumber / 2 + 1, _maxDifficulty);
            ResetBattle();
        }

        public static void ResetBattle()
        {
            StageFacade.CreateNewStage();
            ObjectFacade.CreateNewObjects();
        }
    }
}