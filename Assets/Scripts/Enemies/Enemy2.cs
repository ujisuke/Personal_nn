using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
{
    public class Enemy2 : MonoBehaviour, IObject
    {
        [SerializeField] private Enemy2Parameter enemy2Parameter;
        private HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;

        private void Awake()
        {
            ObjectFacade.AddEnemy(this);
            hP = HP.Initialize(enemy2Parameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(enemy2Parameter, transform.position);
            GetComponent<Enemy2Move>().Initialize(enemy2Parameter);
            GetComponent<Enemy2Attack>().Initialize(enemy2Parameter);
        }

        public void SetReady()
        {
            isReady = true;
        }

        public bool IsDamaging()
        {
            return false;
        }

        public bool IsDead()
        {
            return hP.IsZero();
        }

        public void DamageTo(IObject obj)
        {
            obj.TakeDamage(enemy2Parameter.AttackPower);
        }

        public void TakeDamage(float damage)
        {
            hP = hP.TakeDamage(damage);
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 4f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageFacade._tileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public Vector3 GetRePos3()
        {
            return transform.position;
        }

        public void DestroyObject()
        {
            ObjectFacade.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
