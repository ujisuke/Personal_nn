using UnityEngine;
using Assets.Scripts.Objects;
using Cysharp.Threading.Tasks;
using System;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Dead : MonoBehaviour
    {
        private Enemy3Parameter _enemy3Parameter;
        private Enemy3Main enemy3Main;
        
        public void Initialize(Enemy3Parameter _enemy3Parameter)
        {
            this._enemy3Parameter = _enemy3Parameter;
            enemy3Main = GetComponent<Enemy3Main>();
        }

        private async void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(enemy3Main);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy3Parameter.DeadTime));
            enemy3Main.DestroyDeadObject();
        }
    }
}