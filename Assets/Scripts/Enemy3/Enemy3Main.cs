using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System.Collections;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Main : MonoBehaviour, IObject
    {
        [SerializeField] private Enemy3Parameter enemy3Parameter;
        private HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;

        private void Awake()
        {
            ObjectFacade.AddEnemy(this);
            hP = HP.Initialize(enemy3Parameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(enemy3Parameter, transform.position);
            GetComponent<Enemy3Move>().Initialize(enemy3Parameter);
            GetComponent<Enemy3Attack>().Initialize(enemy3Parameter);
            GetComponent<Enemy3Animation>().Initialize(enemy3Parameter);
        }

        public void SetReady()
        {
            isReady = true;
        }

        public bool IsDead()
        {
            return hP.IsZero();
        }

        public bool IsDamaging()
        {
            return false;
        }

        public void DamageTo(IObject obj)
        {
            obj.TakeDamage(enemy3Parameter.AttackPower);
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

        public Vector3 GetRePos3()
        {
            return transform.position;
        }

        public void DestroyDeadObject()
        {
            ObjectFacade.RemoveEnemy(this);
            StartCoroutine(WaitAndDestroy());
        }

        private IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(enemy3Parameter.DeadTime);
            Destroy(gameObject);
        }

        public void DestroyAliveObject()
        {
            ObjectFacade.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}