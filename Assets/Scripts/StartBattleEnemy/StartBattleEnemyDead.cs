using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyDead : MonoBehaviour
    {
        private ObjectParameter _objectParameter;
        private StartBattleEnemyMain startBattleEnemyMain;
        
        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
            startBattleEnemyMain = GetComponent<StartBattleEnemyMain>();
        }

        private async void OnEnable()
        {
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(startBattleEnemyMain);
            GetComponent<ObjectMove>().Stop();
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.DeadTime));
            startBattleEnemyMain.DestroyDeadObject();
        } 
    }
}