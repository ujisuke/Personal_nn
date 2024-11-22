using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1 : MonoBehaviour, IObject
    {
        [SerializeField] private Enemy1Parameter enemy1Parameter;
        private HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;

        private void Awake()
        {
            ObjectFacade.AddEnemy(this);
            hP = HP.Initialize(enemy1Parameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(enemy1Parameter, transform.position);
            GetComponent<Enemy1Attack>().Initialize(enemy1Parameter);
            GetComponent<Enemy1Move>().Initialize(enemy1Parameter);
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
            obj.TakeDamage(enemy1Parameter.AttackPower);
        }

        public void TakeDamage(float damage)
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

        public void DestroyObject()
        {
            ObjectFacade.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
