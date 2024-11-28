using UnityEngine;
using Unity.Mathematics;
using System.Collections;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;

namespace Assets.Scripts.Battle
{
    public class BattleSetter : MonoBehaviour
    {
        private static bool isResettingBattle = false;

        private void Start()
        {
            ResetBattle();
        }

        private void FixedUpdate()
        {
            if(ObjectFacade.IsEnemyLiving() || isResettingBattle) return;
            BattleData.GoToNextStage();
            ResetBattle();
        }

        public void ResetBattle()
        {
            StartCoroutine(ResetStageAndObjects());
        }

        private static IEnumerator ResetStageAndObjects()
        {
            isResettingBattle = true;
            ObjectFacade.ClearAllObjects();
            StageFacade.CreateNewStage();
            yield return new WaitUntil(() => !StageFacade.IsCreatingStage);
            ObjectFacade.CreateNewObjects();
            isResettingBattle = false;
        }
    }
}
