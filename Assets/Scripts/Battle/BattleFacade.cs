using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Unity.Mathematics;
using System.Collections;

namespace Assets.Scripts.Battle
{
    public class BattleFacade : MonoBehaviour
    {
        private static int difficulty = 1;
        public static int Difficulty => difficulty;
        private static int stageNumber = 1;
        private static readonly int _maxDifficulty = 3;
        private static bool isResettingBattle = false;

        private void Start()
        {
            ResetBattle();
        }

        private void FixedUpdate()
        {
            if(ObjectFacade.IsEnemyLiving() || isResettingBattle) return;
            stageNumber++;
            difficulty = math.min(stageNumber / 2 + 1, _maxDifficulty);
            ResetBattle();
        }

        private void ResetBattle()
        {
            StartCoroutine(ResetStageAndObjects());
        }

        private static IEnumerator ResetStageAndObjects()
        {
            isResettingBattle = true;
            ObjectFacade.RemoveAndDestroyPlayer();
            ObjectFacade.RemoveAndDestroyAllEnemies();

            StageFacade.CreateNewStage();
            yield return new WaitUntil(() => !StageFacade.IsCreatingStage);
            ObjectFacade.CreateNewObjects();
            isResettingBattle = false;
        }
    }
}