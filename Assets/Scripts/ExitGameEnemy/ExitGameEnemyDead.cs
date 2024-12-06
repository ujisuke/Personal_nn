using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.ExitGameEnemy
{
    public class ExitGameEnemyDead : MonoBehaviour
    {
        private ObjectParameter _objectParameter;
        ExitGameEnemyMain exitGameEnemyMain;

        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
            exitGameEnemyMain = GetComponent<ExitGameEnemyMain>();
        }

        private async void OnEnable()
        {
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(exitGameEnemyMain);
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            exitGameEnemyMain.DestroyDeadObject();
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.DeadTime));
            Application.Quit();
        } 
    }
}