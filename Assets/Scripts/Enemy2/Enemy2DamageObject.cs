using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using System.Collections;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2DamageObject : MonoBehaviour, IEnemyMain
    {
        [SerializeField] private ObjectParameter objectParameter;
        private bool isDamaging = false;

        private void Awake()
        {
            ObjectStorage.AddEnemy(this);
            isDamaging = false;
            StartCoroutine(Suicide());
        }

        public void SetReady()
        {

        }

        private IEnumerator Suicide()
        {
            yield return new WaitForSeconds(0.6f);
            isDamaging = true;
            yield return new WaitForSeconds(0.3f);
            DestroyDeadObject();
        }

        public bool IsDamaging()
        {
            return isDamaging;
        }

        public void DamageTo(IEnemyMain obj)
        {
            obj.TakeDamage(objectParameter.AttackPower);
        }

        public void TakeDamage(int damage)
        {

        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 2f, transform.localScale.y / 2f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 2f, transform.localScale.y / 2f, transform.localScale.y / StageFacade.TileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public Vector3 GetRePos3()
        {
            return transform.position;
        }

        public void DestroyDeadObject()
        {
            ObjectStorage.RemoveEnemy(this);
            Destroy(gameObject);
        }

        public void DestroyAliveObject()
        {
            ObjectStorage.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
