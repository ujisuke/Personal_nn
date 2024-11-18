using System.Collections.Generic;
using Assets.Scripts.Stage;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Battle;

namespace Assets.Scripts.Objects
{
    public class ObjectCreator : MonoBehaviour
    {
        private readonly (int i, int j) playerFirstTileIndex = (StageFacade._stageSide - 1, StageFacade._stageSide - 1);
        private readonly int minimumDistanceBetweenPlayerAndEnemy = 4;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private List<GameObject> enemyPrefabLists;

        public void CreateNewObjects()
        {
            ObjectFacade.RemoveAndDestroyPlayer();
            Instantiate(playerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);

            int objectNumber = BattleFacade.Difficulty;
            ObjectFacade.RemoveAndDestroyAllEnemies();
            List<Vector3> enemyRePos3List = ObjectMove.DrawSomeRePos3AtRandom(objectNumber, playerFirstTileIndex, minimumDistanceBetweenPlayerAndEnemy);

            for (int i = 0; i < objectNumber; i++)
            {
                int randomIndex = Random.Range(0, enemyPrefabLists.Count);
                GameObject objectPrefab = enemyPrefabLists[randomIndex];
                Instantiate(objectPrefab, enemyRePos3List[i], Quaternion.identity);
            }

            StartCoroutine(SetReady());
        }

        private static IEnumerator SetReady()
        {
            yield return new WaitForSeconds(0.5f);
            ObjectFacade.SetAllObjectsReady();
        }
    }
}