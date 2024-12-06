using System;
using Assets.Scripts.Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2Dead : MonoBehaviour
    {
        private Enemy2Parameter _enemy2Parameter;
        private Enemy2Main enemy2Main;
        
        public void Initialize(Enemy2Parameter _enemy2Parameter)
        {
            this._enemy2Parameter = _enemy2Parameter;
            enemy2Main = GetComponent<Enemy2Main>();
        }

        private async void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
            GetComponent<SetShadow>().DestroyShadow();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(enemy2Main);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy2Parameter.DeadTime));
            enemy2Main.DestroyDeadObject();
        }
    }
}