using System.Collections.Generic;
using Assets.Scripts.Stage;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectCreator : MonoBehaviour
    {
        private readonly (int i, int j) playerFirstTileIndex = (StageCreator._stageSide - 1, StageCreator._stageSide - 1);
        private readonly int minimumDistanceBetweenPlayerAndEnemy = 4;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private List<GameObject> enemyPrefabLists;

        public void CreateNewObjects(int objectNumber)
        {
            ObjectFacade.RemoveAndDestroyPlayer();
            Instantiate(playerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);

            ObjectFacade.RemoveAndDestroyAllEnemies();
            List<Vector3> enemyRePos3List = ObjectMove.DrawSomeRePos3AtRandom(objectNumber, playerFirstTileIndex, minimumDistanceBetweenPlayerAndEnemy);

            for (int i = 0; i < objectNumber; i++)
            {
                int randomIndex = Random.Range(0, enemyPrefabLists.Count);
                GameObject objectPrefab = enemyPrefabLists[randomIndex];
                Instantiate(objectPrefab, enemyRePos3List[i], Quaternion.identity);
            }
        }
    }
}