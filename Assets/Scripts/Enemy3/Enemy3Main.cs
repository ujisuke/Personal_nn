using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Main : MonoBehaviour, IEnemyMain
    {
        [SerializeField] private Enemy3Parameter _enemy3Parameter;
        private HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;

        private void Awake()
        {
            ObjectStorage.AddEnemy(this);
            hP = HP.Initialize(_enemy3Parameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(_enemy3Parameter, transform.position);
            GetComponent<Enemy3Move>().Initialize(_enemy3Parameter);
            GetComponent<Enemy3Attack>().Initialize(_enemy3Parameter);
            GetComponent<Enemy3Animation>().Initialize(_enemy3Parameter);
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
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy3Parameter.DeadTime));
            Destroy(gameObject);
        }

        public void KillAliveObject()
        {
            hP = hP.GetZero();
        }
    }
}
