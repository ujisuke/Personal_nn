using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyMain : MonoBehaviour, IEnemyMain
    {
        [SerializeField] private ObjectParameter _objectParameter;
        private HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;

        private void Awake()
        {
            ObjectStorage.AddEnemy(this);
            hP = HP.Initialize(_objectParameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(_objectParameter, transform.position);
            GetComponent<StartBattleEnemyMove>().Initialize();
            GetComponent<StartBattleEnemyDead>().Initialize(_objectParameter);
            GetComponent<StartBattleEnemyAnimation>().Initialize(_objectParameter);
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
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.DeadTime));
            Destroy(gameObject);
        }

        public void DestroyAliveObject()
        {
            ObjectStorage.RemoveEnemy(this); 
            Destroy(gameObject);
        }
    }
}
