using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1Main : MonoBehaviour, IEnemyMain
    {
        [SerializeField] private Enemy1Parameter _enemy1Parameter;
        private HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;

        private void Awake()
        {
            ObjectStorage.AddEnemy(this);
            hP = HP.Initialize(_enemy1Parameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(_enemy1Parameter, transform.position);
            GetComponent<Enemy1Attack>().Initialize(_enemy1Parameter);
            GetComponent<Enemy1Move>().Initialize(_enemy1Parameter);
            GetComponent<Enemy1Animation>().Initialize(_enemy1Parameter);
        }

        public void SetReady()
        {
            isReady = true;
        }
        
        public bool IsDead()
        {
            return hP.IsZero();
        }

        public void TakeDamage(int damage)
        {
            hP = hP.TakeDamage(damage);
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 4f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageFacade.TileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }
        
        public async void DestroyDeadObject()
        {
            ObjectStorage.RemoveEnemy(this);
            ObjectStorage.RemoveAndDestroyEnemyDamageObjects(this);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            Destroy(gameObject);
        }

        public void KillAliveObject()
        {
            hP = hP.GetZero();
        }
    }
}
