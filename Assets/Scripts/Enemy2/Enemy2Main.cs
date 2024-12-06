using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2Main : MonoBehaviour, IEnemyMain
    {
        [SerializeField] private Enemy2Parameter _enemy2Parameter;
        private HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;

        private void Awake()
        {
            ObjectStorage.AddEnemy(this);
            hP = HP.Initialize(_enemy2Parameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(_enemy2Parameter, transform.position);
            GetComponent<Enemy2Move>().Initialize(_enemy2Parameter);
            GetComponent<Enemy2Attack>().Initialize(_enemy2Parameter);
            GetComponent<Enemy2MissingPlayer>().Initialize(_enemy2Parameter);
            GetComponent<Enemy2Dead>().Initialize(_enemy2Parameter);
            GetComponent<Enemy2Animation>().Initialize(_enemy2Parameter);
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
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy2Parameter.DeadTime));
            Destroy(gameObject);
        }

        public void DestroyAliveObject()
        {
            ObjectStorage.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
