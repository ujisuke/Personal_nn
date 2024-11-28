using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1Main : MonoBehaviour, IEnemyMain
    {
        [SerializeField] private Enemy1Parameter _enemy1Parameter;
        private HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;
        public List<IEnemyMain> DamageObjects = new();

        private void Awake()
        {
            ObjectFacade.AddEnemy(this);
            hP = HP.Initialize(_enemy1Parameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(_enemy1Parameter, transform.position);
            GetComponent<Enemy1Attack>().Initialize(_enemy1Parameter);
            GetComponent<Enemy1Move>().Initialize(_enemy1Parameter);
            GetComponent<Enemy1Dead>().Initialize(_enemy1Parameter);
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
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageFacade._TileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }
        
        public void DestroyDeadObject()
        {
            ObjectFacade.RemoveEnemy(this);
            StartCoroutine(WaitAndDestroy());
        }

        private IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(_enemy1Parameter.DeadTime);
            Destroy(gameObject);
        }

        public void DestroyAliveObject()
        {
            ObjectFacade.RemoveEnemy(this); 
            Destroy(gameObject);
        }
    }
}
