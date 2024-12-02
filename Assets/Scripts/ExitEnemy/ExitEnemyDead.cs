using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.ExitEnemy
{
    public class ExitEnemyDead : MonoBehaviour
    {
        private ObjectParameter _objectParameter;
        private ExitEnemyMain exitEnemyMain;
        
        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
            exitEnemyMain = GetComponent<ExitEnemyMain>();
        }

        private async void OnEnable()
        {
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(exitEnemyMain);
            GetComponent<ObjectMove>().Stop();
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.DeadTime));
            exitEnemyMain.DestroyDeadObject();
        } 
    }
}