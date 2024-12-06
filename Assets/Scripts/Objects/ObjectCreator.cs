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
        private static readonly (int i, int j) playerFirstTileIndex = (StageFacade.StageSide - 1, StageFacade.StageSide - 1);
        private static readonly int minimumDistanceBetweenPlayerAndEnemy = 4;
        [SerializeField] private GameObject _playerPrefab;
        private static GameObject _singletonPlayerPrefab;
        [SerializeField] private List<GameObject> _enemyPrefabList;
        private static List<GameObject> _singletonEnemyPrefabList;
        [SerializeField] private GameObject _enemyDamageObjectPrefab;
        private static GameObject _singletonEnemyDamageObjectPrefab;
        [SerializeField] private GameObject _startBattleEnemyPrefab;
        private static GameObject _singletonStartBattleEnemyPrefab;
        [SerializeField] private GameObject _resetDataEnemyPrefab;
        private static GameObject _singletonResetDataEnemyPrefab;
        [SerializeField] private GameObject _setGameEnemyPrefab;
        private static GameObject _singletonSetGameEnemyPrefab;
        [SerializeField] private GameObject _exitGameEnemyPrefab;
        private static GameObject _singletonExitGameEnemyPrefab;
        [SerializeField] private GameObject _backToLobbyEnemyPrefab;
        private static GameObject _singletonBackToLobbyEnemyPrefab;


        private void Awake()
        {
            _singletonPlayerPrefab = _playerPrefab;
            _singletonEnemyPrefabList = _enemyPrefabList;
            _singletonEnemyDamageObjectPrefab = _enemyDamageObjectPrefab;
            _singletonStartBattleEnemyPrefab = _startBattleEnemyPrefab;
            _singletonResetDataEnemyPrefab = _resetDataEnemyPrefab;
            _singletonSetGameEnemyPrefab = _setGameEnemyPrefab;
            _singletonExitGameEnemyPrefab = _exitGameEnemyPrefab;
            _singletonBackToLobbyEnemyPrefab = _backToLobbyEnemyPrefab;
        }

        public static async UniTask CreateLobbyObjects()
        {
            await InstantiateAsync(_singletonPlayerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);
            await InstantiateAsync(_singletonStartBattleEnemyPrefab, ObjectMove.ConvertToRePos3FromTileIndex((StageFacade.StageSide - 3, 0)), Quaternion.identity);
            await InstantiateAsync(_singletonResetDataEnemyPrefab, ObjectMove.ConvertToRePos3FromTileIndex((2, 0)), Quaternion.identity);
            await InstantiateAsync(_singletonSetGameEnemyPrefab, ObjectMove.ConvertToRePos3FromTileIndex((0, 2)), Quaternion.identity);
            await InstantiateAsync(_singletonExitGameEnemyPrefab, ObjectMove.ConvertToRePos3FromTileIndex((0, StageFacade.StageSide - 2)), Quaternion.identity);
            ObjectStorage.SetAllObjectsReady();
        }

        public static async UniTask CreateSettingObjects()
        {
            await InstantiateAsync(_singletonPlayerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);
            await InstantiateAsync(_singletonBackToLobbyEnemyPrefab, ObjectMove.ConvertToRePos3FromTileIndex((0, 0)), Quaternion.identity);
            ObjectStorage.SetAllObjectsReady();
        }

        public static async UniTask CreateBattleObjects()
        {
            await InstantiateAsync(_singletonPlayerPrefab, ObjectMove.ConvertToRePos3FromTileIndex(playerFirstTileIndex), Quaternion.identity);

            int objectCount = math.min(BattleFacade.EnemyDifficulty, StageFacade.StageSide * StageFacade.StageSide - minimumDistanceBetweenPlayerAndEnemy * minimumDistanceBetweenPlayerAndEnemy);
            List<Vector3> enemyRePos3List = ObjectMove.DrawSomeRePos3AtRandom(objectCount, playerFirstTileIndex, minimumDistanceBetweenPlayerAndEnemy, StageFacade.StageSide);

            for (int i = 0; i < objectCount; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, _singletonEnemyPrefabList.Count);
                GameObject objectPrefab = _singletonEnemyPrefabList[randomIndex];
                await InstantiateAsync(objectPrefab, enemyRePos3List[i], Quaternion.identity);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            ObjectStorage.SetAllObjectsReady();
        }
        
        public static void InstantiateEnemyDamageObject(Vector3 rePos3, EnemyDamageObjectParameter enemyDamageObjectParameter, IEnemyMain enemy)
        {
            GameObject newDamageObject = Instantiate(_singletonEnemyDamageObjectPrefab, rePos3, Quaternion.identity);
            newDamageObject.GetComponent<EnemyDamageObjectMain>().Initialize(enemyDamageObjectParameter, enemy).Forget();
        }
    }
}