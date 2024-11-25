using System.Collections.Generic;
using Assets.Scripts.Stage;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Battle;
using Assets.Scripts.Enemies;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Objects
{
    public class ObjectCreator : MonoBehaviour
    {
        private readonly (int i, int j) playerFirstTileIndex = (StageFacade._StageSide - 1, StageFacade._StageSide - 1);
        private readonly int minimumDistanceBetweenPlayerAndEnemy = 4;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private List<GameObject> enemyPrefabLists;

        public void CreateNewObjects()
        {
            Instantiate(playerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);

            int objectNumber = BattleFacade.Difficulty;            
            List<Vector3> enemyRePos3List = ObjectMove.DrawSomeRePos3AtRandom(objectNumber, playerFirstTileIndex, minimumDistanceBetweenPlayerAndEnemy, StageFacade._StageSide);

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
        
        public static void InstantiateDamageObject(GameObject damageObject, Vector3 rePos3, DamageObjectParameter damageObjectParameter)
        {
            GameObject newDamageObject = Instantiate(damageObject, rePos3, Quaternion.identity);
            newDamageObject.GetComponent<EnemyDamageObject>().Initialize(damageObjectParameter);
        }
    }
}