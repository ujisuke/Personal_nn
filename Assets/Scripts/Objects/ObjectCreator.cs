using System.Collections.Generic;
using Assets.Scripts.Stage;
using UnityEngine;
using Assets.Scripts.Battle;
using Assets.Scripts.EnemyDamageObject;
using Assets.ScriptableObjects;
using Unity.Mathematics;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Objects
{
    public class ObjectCreator : MonoBehaviour
    {
        private static readonly (int i, int j) playerFirstTileIndex = (StageCreator._StageSide - 1, StageCreator._StageSide - 1);
        private static readonly int minimumDistanceBetweenPlayerAndEnemy = 4;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private List<GameObject> _enemyPrefabList;
        [SerializeField] private GameObject _enemyDamageObjectPrefab;
        [SerializeField] private GameObject _startBattleEnemyPrefab;
        [SerializeField] private GameObject _resetDataEnemyPrefab;
        [SerializeField] private GameObject _exitGameEnemyPrefab;
        [SerializeField] private GameObject _backToRobbyEnemyPrefab;
        private static ObjectCreator singletonInstance;
        public static ObjectCreator SingletonInstance => singletonInstance;


        private void Awake()
        {
            singletonInstance = this;
        }

        public async UniTask CreateLobbyObjects()
        {
            await InstantiateAsync(_playerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);
            await InstantiateAsync(_startBattleEnemyPrefab, ObjectMove.ConvertToRePos3FromTileIndex((0, 1)), Quaternion.identity);
            await InstantiateAsync(_resetDataEnemyPrefab, ObjectMove.ConvertToRePos3FromTileIndex((0, 3)), Quaternion.identity);
            await InstantiateAsync(_exitGameEnemyPrefab, ObjectMove.ConvertToRePos3FromTileIndex((0, StageCreator._StageSide - 2)), Quaternion.identity);
            ObjectStorage.SetAllObjectsReady();
        }

        public async UniTask CreateClearObjects()
        {
            await InstantiateAsync(_playerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);
            await InstantiateAsync(_backToRobbyEnemyPrefab, ObjectMove.ConvertToRePos3FromTileIndex((0, 1)), Quaternion.identity);
            ObjectStorage.SetAllObjectsReady();
        }

        public async UniTask CreateBattleObjects()
        {
            await InstantiateAsync(_playerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);

            int objectCount = math.min(BattleData.EnemyDifficulty, StageCreator._StageSide * StageCreator._StageSide - minimumDistanceBetweenPlayerAndEnemy * minimumDistanceBetweenPlayerAndEnemy);
            List<Vector3> enemyRePos3List = ObjectMove.DrawSomeRePos3AtRandom(objectCount, playerFirstTileIndex, minimumDistanceBetweenPlayerAndEnemy, StageCreator._StageSide);

            UnityEngine.Random.InitState(BattleData.BattleSeed);
            for (int i = 0; i < objectCount; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, _enemyPrefabList.Count);
                GameObject objectPrefab = _enemyPrefabList[randomIndex];
                await InstantiateAsync(objectPrefab, enemyRePos3List[i], Quaternion.identity);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            ObjectStorage.SetAllObjectsReady();
        }
        
        public void InstantiateEnemyDamageObject(Vector3 rePos3, EnemyDamageObjectParameter enemyDamageObjectParameter, EnemyMain enemy)
        {
            GameObject newDamageObject = Instantiate(_enemyDamageObjectPrefab, rePos3, Quaternion.identity);
            newDamageObject.GetComponent<EnemyDamageObjectMain>().Initialize(enemyDamageObjectParameter, enemy).Forget();
        }
    }
}