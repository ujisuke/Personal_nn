using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1Dead : MonoBehaviour
    {
        private Enemy1Parameter _enemy1Parameter;
        private Enemy1Main enemy1Main;
        
        public void Initialize(Enemy1Parameter _enemy1Parameter)
        {
            this._enemy1Parameter = _enemy1Parameter;
            enemy1Main = GetComponent<Enemy1Main>();
        }

        private async void OnEnable()
        {
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(enemy1Main);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy1Parameter.DeadTime));
            enemy1Main.DestroyDeadObject();
        }
    }
}