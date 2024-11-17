using System.Collections.Generic;
using Assets.Scripts.Stage;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectCreator : MonoBehaviour
    {
        private readonly (int i, int j) playerFirstTileIndex = (StageCreator._stageSide - 1, StageCreator._stageSide - 1);
        private readonly int minimumDistanceBetweenPlayerAndEnemy = 4;
        [SerializeField] private List<GameObject> objectPrefabLists;

        public void CreateNewObjects(int objectNumber)
        {
            ObjectFacade.RemoveAllEnemies();
            List<Vector3> objectRePos3List = ObjectMove.DrawSomeRePos3AtRandom(objectNumber, playerFirstTileIndex, minimumDistanceBetweenPlayerAndEnemy);

            for (int i = 0; i < objectNumber; i++)
            {
                int randomIndex = Random.Range(0, objectPrefabLists.Count);
                GameObject objectPrefab = objectPrefabLists[randomIndex];
                Instantiate(objectPrefab, objectRePos3List[i], Quaternion.identity);
            }
        }
    }
}