using System.Collections.Generic;
using Assets.Scripts.Stage;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Battle;
using Assets.Scripts.EnemyDamageObject;
using Assets.ScriptableObjects;
using Unity.Mathematics;

namespace Assets.Scripts.Objects
{
    public class ObjectCreator : MonoBehaviour
    {
        private readonly (int i, int j) playerFirstTileIndex = (StageFacade.StageSide - 1, StageFacade.StageSide - 1);
        private readonly int minimumDistanceBetweenPlayerAndEnemy = 4;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private List<GameObject> enemyPrefabLists;

        public void CreateNewObjects()
        {
            Instantiate(playerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);

            int objectCount = math.min(BattleFacade.Difficulty, StageFacade.StageSide * StageFacade.StageSide - minimumDistanceBetweenPlayerAndEnemy * minimumDistanceBetweenPlayerAndEnemy);
            List<Vector3> enemyRePos3List = ObjectMove.DrawSomeRePos3AtRandom(objectCount, playerFirstTileIndex, minimumDistanceBetweenPlayerAndEnemy, StageFacade.StageSide);

            for (int i = 0; i < objectCount; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, enemyPrefabLists.Count);
                GameObject objectPrefab = enemyPrefabLists[randomIndex];
                Instantiate(objectPrefab, enemyRePos3List[i], Quaternion.identity);
            }

            StartCoroutine(SetReady());
        }

        private static IEnumerator SetReady()
        {
            yield return new WaitForSeconds(0.5f);
            ObjectStorage.SetAllObjectsReady();
        }
        
        public static void InstantiateDamageObject(GameObject damageObject, Vector3 rePos3, DamageObjectParameter damageObjectParameter, IEnemyMain enemy)
        {
            GameObject newDamageObject = Instantiate(damageObject, rePos3, Quaternion.identity);
            newDamageObject.GetComponent<EnemyDamageObjectMain>().Initialize(damageObjectParameter, enemy);
        }
    }
}