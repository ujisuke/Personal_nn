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

        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
        }

        private async void OnEnable()
        {
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            GetComponent<ExitGameEnemyMain>().DestroyDeadObject();
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.DeadTime));
            Application.Quit();
        } 
    }
}